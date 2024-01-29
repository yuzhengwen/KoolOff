using System;
using UnityEngine;

public class Heart : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject cupidsArrowPrefab;
    public void OnCollect(GameObject player)
    {
        player.GetComponent<PlayerCollector>().AddItemToInventory(this);
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
