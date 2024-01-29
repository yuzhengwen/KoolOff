using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public GameObject player; // player that sent the projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("HIT");
            collision.GetComponent<PlayerMovement>().SetDebuff(PlayerState.Feared, player);
            Destroy(gameObject);
        }
    }
}
