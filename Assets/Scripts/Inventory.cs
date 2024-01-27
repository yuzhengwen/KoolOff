using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private Queue<ICollectible> items;
    private readonly int maxItems = 2;
    private UI_Inventory uiInventory;
    private RotatingAim rotatingAim;
    private void Start()
    {
        items = new();
        uiInventory = transform.parent.GetComponent<PlayersManager>().uiInventory;
        rotatingAim = GetComponentInChildren<RotatingAim>();
        UpdateRotatingAim();
    }

    private void UpdateRotatingAim()
    {
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
        uiInventory.UpdateUI(gameObject, this);
        UpdateRotatingAim();
    }
    public void UseItem(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
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
        UpdateRotatingAim();
    }
    public Queue<ICollectible> GetItems()
    {
        return items;
    }
}