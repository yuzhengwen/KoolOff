using System;
using UnityEngine;

public class Feather : MonoBehaviour, ICollectible
{
    public static event Action<ICollectible> OnCollected;
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject featherProjectilePrefab;
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
        if (!player.activeSelf) return; 
        GameObject projectile = Instantiate(featherProjectilePrefab, rotatingAim.transform.position, rotatingAim.GetRotation());
        projectile.GetComponent<Hitbox>().player = player;
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());   
    }
}
