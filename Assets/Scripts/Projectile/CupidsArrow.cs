using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupidsArrow : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 8f;
    void Start()
    {
        direction = transform.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().SetDebuff(PlayerDebuff.Tickled);
            Destroy(gameObject);
        }
    }
}
