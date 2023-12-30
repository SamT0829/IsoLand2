using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    public Text itemNameText;

    public void UpdateItemName(ItemName itemName){
        itemNameText.text = itemName switch{
            ItemName.Key => "信箱鑰匙",
            ItemName.Ticket => "一張船票",
            _ => ""
        };
    }
}
