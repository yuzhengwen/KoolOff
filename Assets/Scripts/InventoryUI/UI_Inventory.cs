using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Inventory : MonoBehaviour
{
    private Dictionary<GameObject, UI_Slot[]> slotsDict = new();

    [SerializeField] private GameObject playerDisplayPrefab;
    private readonly int noOfSlots = 2;

    private readonly List<Inventory> inventories = new(); // all inventories that are being tracked
    public void AddPlayer(PlayerInput playerInput)
    {
        Debug.Log("Add player");
        GameObject playerObj = playerInput.gameObject;
        TrackInventory(playerObj);
        AddPlayerSlots(playerObj);
    }

    private void AddPlayerSlots(GameObject playerObj)
    {
        UI_Slot[] slots = new UI_Slot[noOfSlots];
        GameObject playerDisplayObj = transform.GetChild(inventories.Count - 1).gameObject;
        for (int j = 0; j < noOfSlots; j++)
        {
            slots[j] = playerDisplayObj.transform.GetChild(0).GetChild(j).GetComponent<UI_Slot>();
        }
        slotsDict.Add(playerObj, slots);
    }

    private void TrackInventory(GameObject playerObj)
    {
        Inventory playerInv = playerObj.GetComponentInChildren<Inventory>();
        inventories.Add(playerInv);
        playerInv.OnInventoryChanged += UpdateUI;
    }

    public void UpdateUI(GameObject playerObj, Inventory inventory)
    {
        ICollectible[] items = inventory.GetItems().ToArray();
        UI_Slot[] slots = slotsDict[playerObj];
        foreach (UI_Slot slot in slots)
        {
            slot.ClearSlot();
        }
        for (int i = 0; i < items.Length; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }
    private void OnDisable()
    {
        foreach (Inventory i in inventories)
        {
            i.OnInventoryChanged -= UpdateUI;
        }
    }
}
