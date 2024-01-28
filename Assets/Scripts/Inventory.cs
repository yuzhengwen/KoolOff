using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private Queue<ICollectible> items;
    private readonly int maxItems = 2;
    private RotatingAim rotatingAim;
    public event Action<GameObject, Inventory> OnInventoryChanged;
 
    private void Start()
    {
        items = new();
        rotatingAim = transform.parent.gameObject.GetComponentInChildren<RotatingAim>();
        UpdateRotatingAim();
    }

    private void UpdateRotatingAim()
    {
        if (rotatingAim == null)
            rotatingAim = transform.parent.gameObject.GetComponentInChildren<RotatingAim>();
        if (items.Count > 0)
        {
            if (!rotatingAim.isActiveAndEnabled)
                rotatingAim.gameObject.SetActive(true);
        }
        else
        {
            if (rotatingAim.isActiveAndEnabled)
                rotatingAim.gameObject.SetActive(false);
        }
    }

    public void AddItem(ICollectible item)
    {
        if (items.Count >= maxItems)
            items.Dequeue();
        items.Enqueue(item);
        OnInventoryChanged?.Invoke(transform.parent.gameObject, this);
        UpdateRotatingAim();
        Debug.Log($"Added item to Inventory of {transform.parent.gameObject.name}");
    }
    public void UseItem(GameObject player)
    {
        if (items.Count > 0)
        {
            ICollectible item = items.Dequeue();
            item.Use(player, rotatingAim);
        }
        else
        {
            Debug.Log("No items in inventory");
        }
        OnInventoryChanged?.Invoke(player, this);
        UpdateRotatingAim();
    }
    public Queue<ICollectible> GetItems()
    {
        return items;
    }
}