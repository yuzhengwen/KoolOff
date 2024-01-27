using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Queue<ICollectible> items;
    private readonly int maxItems = 2;
    [SerializeField] private UI_Inventory uiInventory;
    private void Awake()
    {
        items = new();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
    }

    public void AddItem(ICollectible item)
    {
        if(items.Count >= maxItems)
            items.Dequeue();
        items.Enqueue(item);
        uiInventory.UpdateUI(gameObject, this);
    }
    public void UseItem()
    {
        if (items.Count > 0)
        {
            ICollectible item = items.Dequeue();
            item.Use();
        }
        else
        {
            Debug.Log("No items in inventory");
        }
        uiInventory.UpdateUI(gameObject, this);
    }
    public Queue<ICollectible> GetItems()
    {
        return items;
    }
}