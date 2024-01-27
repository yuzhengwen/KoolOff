using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, ICollectible
{
    public static event Action<ICollectible> OnCollected;
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject cupidsArrowPrefab;
    public void OnCollect()
    {
        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }
    public ItemSO GetItemSO()
    {
        return itemSO;
    }
    public void Use()
    {
        Debug.Log($"Used {itemSO.abilityName}");
        GameObject arrow = Instantiate(cupidsArrowPrefab, transform.position, Quaternion.identity);
    }
}
