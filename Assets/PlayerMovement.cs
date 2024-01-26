using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private int movementForce = 2;
    private float maxSpeed = 5;

    [SerializeField] private TilesManager tilesManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //// temporary movement
        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * 5, transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * 5, transform.position.z);
        //Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //rb.AddForce(movement * movementForce);
        //Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        //Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector3Int tilePos = tilesManager.tilemap.WorldToCell(mousePos);
        //    tilesManager.DestroyTile(tilePos);
        //    Debug.Log("Tile destroyed at " + tilePos);
        //}
        GetTileBelowPlayer();
    }

    public LayerMask terrain;
    private Vector3Int GetTileBelowPlayer()
    {
        //RaycastHit2D[] hits;
        //hits = Physics2D.RaycastAll(transform.position, transform.forward, 10.0F);

        //foreach (RaycastHit2D hit in hits)
        //{
        //    Tile tile = hit.collider.GetComponent<Tile>();
        //    if (tile != null)
        //    {
        //        return hit.collider.transform.position;
        //    }
        //}
        Vector3Int tilePos = tilesManager.tilemap.WorldToCell(rb.position);
        Debug.Log("Tile below player: " + tilePos); 
        tilesManager.tilemap.SetTileFlags(tilePos, TileFlags.None);
        tilesManager.tilemap.SetColor(tilePos, Color.red);
        tilesManager.DestroyTileDelayed(tilePos);
        return tilePos;
    }
}
