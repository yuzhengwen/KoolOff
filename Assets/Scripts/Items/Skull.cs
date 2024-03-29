using System;
using UnityEngine;

public class Skull : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject hitboxPrefab;
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
        GameObject hitbox = Instantiate(hitboxPrefab, rotatingAim.transform.position, rotatingAim.GetRotation());
        Destroy(hitbox, 0.2f);
        hitbox.GetComponent<Hitbox>().player = player;
        Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }
}
