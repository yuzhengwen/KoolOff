using System;
using UnityEngine;

public class Heart : MonoBehaviour, ICollectible
{
    public static event Action<ICollectible> OnCollected;
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject cupidsArrowPrefab;
    public void OnCollect(GameObject player)
    {
        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }
    public ItemSO GetItemSO()
    {
        return itemSO;
    }
    public void Use(GameObject player, RotatingAim rotatingAim)
    {
        GameObject arrow = Instantiate(cupidsArrowPrefab, rotatingAim.transform.position, rotatingAim.GetRotation());
        arrow.GetComponent<CupidsArrow>().player = player;
        Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }
}
