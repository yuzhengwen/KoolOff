using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherProjectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 1.5f;
    public GameObject player; // player that sent the projectile
    private void Start()
    {
        direction = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().SetDebuff(PlayerState.Tickled, player);
            Destroy(gameObject);
        }
    }
}
