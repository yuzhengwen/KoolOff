using System;
using UnityEngine;

public class Feather : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject featherProjectilePrefab;
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
        if (!player.activeSelf) return; 
        GameObject projectile = Instantiate(featherProjectilePrefab, rotatingAim.transform.position, rotatingAim.GetRotation());
        projectile.GetComponent<FeatherProjectile>().player = player;
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());   
    }
}
