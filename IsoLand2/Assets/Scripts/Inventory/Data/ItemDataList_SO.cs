using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList_SO", order = 0)]
public class ItemDataList_SO : ScriptableObject {
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(ItemName itemName){
        return itemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]                          //System.Serializable inspector面板內顯示自定義資料型別類例項物件的內部資料
public class ItemDetails{
    public ItemName itemName;
    public Sprite itemSprite;
}