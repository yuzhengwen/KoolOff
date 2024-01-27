using System;
using UnityEngine;

public class Feather : MonoBehaviour, ICollectible
{
    public static event Action<ICollectible> OnCollected;
    [SerializeField] private ItemSO itemSO;
    public void OnCollect(GameObject player)
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
    }
}
