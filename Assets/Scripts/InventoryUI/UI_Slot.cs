using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    private ICollectible item;
    private Image itemDisplay;
    private void Awake()
    {
        itemDisplay = transform.GetChild(0).GetComponent<Image>();
        itemDisplay.enabled = false;
    }
    public void SetItem(ICollectible item)
    {
        this.item = item;
        if (item == null)
            ClearSlot();
        else
        {
            itemDisplay.enabled = true;
            itemDisplay.sprite = item.GetItemSO().itemSprite;
        }
    }
    public void ClearSlot()
    {
        itemDisplay.enabled = false;
    }
}
