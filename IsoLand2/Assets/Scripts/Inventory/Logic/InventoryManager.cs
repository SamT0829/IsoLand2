using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>, ISaveable
{
    public ItemDataList_SO itemData;
    [SerializeField] private List<ItemName> itemList = new List<ItemName>();

    private void OnEnable() {
        EvenHandler.ItemUseEvent += OnItemUseEvent;
        EvenHandler.ChangeItemEvent += OnChangeItemEven;
        EvenHandler.AfterSceneloadedEvent += OnAfterSceneloadedEvent;
        EvenHandler.StartNewGameEvent += OnStartNewGameEvent;  
    }

    private void OnDisable(){
        EvenHandler.ItemUseEvent -= OnItemUseEvent;
        EvenHandler.ChangeItemEvent -= OnChangeItemEven;
        EvenHandler.AfterSceneloadedEvent += OnAfterSceneloadedEvent;
        EvenHandler.StartNewGameEvent -= OnStartNewGameEvent;  
    }

    private void Start() {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }

    private void OnAfterSceneloadedEvent()
    {
        if(itemList.Count == 0)
            EvenHandler.CallUpdateUIEvent(null, -1);        
        else{
            for(int i = 0; i < itemList.Count; i++){
                EvenHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }           
    }

    private void OnChangeItemEven(int index)
    {
        if(index >= 0 && index < itemList.Count){
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EvenHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnItemUseEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);

        if(itemList.Count == 0){
            EvenHandler.CallUpdateUIEvent(null, -1);
        }
    }

    public void AddItem(ItemName itemName){
        if(!itemList.Contains(itemName)){
            itemList.Add(itemName);
            //UI 對應顯示
            EvenHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }
    }

    private int GetItemIndex(ItemName itemName){
        for(int i = 0; i < itemList.Count; i++){
            if(itemList[i] == itemName)
                return i;
        }
        return -1;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}
