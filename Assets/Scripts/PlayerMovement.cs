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
    private float speed = 3;
    private Vector3Int tilePos;

    [SerializeField] private TilesManager tilesManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //// temporary movement
        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed, transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed, transform.position.z);
    }
    private void Update()
    {
        tilePos = GetTileBelowPlayer();
        Debug.Log("Tile below player: " + tilePos);
        if (!tilesManager.tilemap.HasTile(tilePos))
            Debug.Log("Player Falling");
        else
        {
            tilesManager.tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilesManager.tilemap.SetColor(tilePos, Color.red);
            tilesManager.DestroyTileDelayed(tilePos);
        }
    }

    public LayerMask terrain;
    private Vector3Int GetTileBelowPlayer()
    {
        return tilesManager.tilemap.WorldToCell(rb.position);
    }
}
