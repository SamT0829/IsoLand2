using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public ItemToolTip toolTip;
    private ItemDetails currentItem;
    private bool isSelected;

    public void SetItem(ItemDetails itemDetails){
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty(){
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        EvenHandler.CallItemSelectedEvent(currentItem, isSelected);
    }  

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.activeInHierarchy){
            toolTip.gameObject.SetActive(true);
            toolTip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }
}
