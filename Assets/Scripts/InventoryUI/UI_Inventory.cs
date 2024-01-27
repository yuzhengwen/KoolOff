using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Dictionary<GameObject, UI_Slot[]> slotsDict = new();

    [SerializeField] private GameObject playerDisplayPrefab;
    [SerializeField] private GameObject playerParent;
    private int noOfPlayers;
    private readonly int noOfSlots = 2;

    void Start()
    {
        noOfPlayers = playerParent.transform.childCount;
        Debug.Log(noOfPlayers);
        InstantiateHUD();
    }

    private void InstantiateHUD()
    {
        GameObject[] playerObjs = new GameObject[noOfPlayers];
        for (int i = 0; i < noOfPlayers; i++)
        {
            // create an array of 2 slots per player
            UI_Slot[] slots = new UI_Slot[noOfSlots];
            GameObject playerDisplay = Instantiate(playerDisplayPrefab, transform);
            playerDisplay.SetActive(true);

            for (int j = 0; j < noOfSlots; j++)
            {
                slots[j] = playerDisplay.transform.GetChild(0).GetChild(j).GetComponent<UI_Slot>();
            }
            // get list of all players (key)
            playerObjs[i] = playerParent.transform.GetChild(i).gameObject;
            slotsDict.Add(playerObjs[i], slots);
        }
    }

    public void UpdateUI(GameObject playerObj, Inventory inventory)
    {
        for (int i = 0; i < noOfSlots; i++)
        {
            slotsDict[playerObj][i].SetItem(inventory.GetItems().Dequeue());
        }
    }
}
