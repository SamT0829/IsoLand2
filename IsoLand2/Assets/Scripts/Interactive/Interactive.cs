using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone;
    public void CheckItem(ItemName itemName){
        if(requireItem == itemName){
            isDone = true;
            //使用這物品, 移除物品
            OnClickedAction();
            EvenHandler.CallItemUsedEvent(itemName);
        }
    }

    /// <summary>
    /// 默認是正確物品的情況執行
    /// </summary>
    protected virtual void OnClickedAction(){

    }

    public virtual void EmptyClicked(){
        Debug.Log("空點");
    }
}
