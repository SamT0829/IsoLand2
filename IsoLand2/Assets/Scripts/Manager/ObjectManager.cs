using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, ISaveable
{
    private Dictionary<ItemName, bool> itemAvaibleDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    private void OnEnable() {
        EvenHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EvenHandler.AfterSceneloadedEvent += OnAfterSceneloadedEvent;
        EvenHandler.UpdateUIEvent += OnUpdateUIEvent;
        EvenHandler.StartNewGameEvent += OnStartNewGameEvent;  
    }

    private void OnDisable() {
        EvenHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EvenHandler.AfterSceneloadedEvent -= OnAfterSceneloadedEvent;
        EvenHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EvenHandler.StartNewGameEvent -= OnStartNewGameEvent;  
    }

    private void Start() {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        itemAvaibleDict.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforeSceneUnloadEvent(){
        foreach(var item in FindObjectsOfType<Item>()){
            if(!itemAvaibleDict.ContainsKey(item.itemName)){
                itemAvaibleDict.Add(item.itemName, true);
            }
        }

        foreach(var item in FindObjectsOfType<Interactive>()){
            if(interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void OnAfterSceneloadedEvent(){
        foreach(var item in FindObjectsOfType<Item>()){
            if(!itemAvaibleDict.ContainsKey(item.itemName)){
                itemAvaibleDict.Add(item.itemName, true);
            }
            else
                item.gameObject.SetActive(itemAvaibleDict[item.itemName]);
        }

        foreach(var item in FindObjectsOfType<Interactive>()){
            if(interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }
    
    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2){
        if(itemDetails != null){
            itemAvaibleDict[itemDetails.itemName] = false;
        }
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvaibleDict = this.itemAvaibleDict;
        saveData.interactiveStateDict = this.interactiveStateDict;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvaibleDict = saveData.itemAvaibleDict;
        this.interactiveStateDict = saveData.interactiveStateDict;
    }
}
