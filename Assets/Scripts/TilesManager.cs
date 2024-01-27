using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesManager : MonoBehaviour
{
    public Tilemap tilemap;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    
    public void DestroyTileDelayed(Vector3Int pos, float delay = 1.0f)
    {
        StartCoroutine(DestroyTileCo(pos, delay));
    }

    IEnumerator DestroyTileCo(Vector3Int pos, float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyTile(pos);
    }
    public void DestroyTile(Vector3Int pos)
    {
        Tile tile = tilemap.GetTile<Tile>(pos);
        if (tile != null)
        {
            tilemap.SetTile(pos, null);
            StartCoroutine(RestoreTileCo(tile, pos));
        }
    }
    IEnumerator RestoreTileCo(Tile tile, Vector3Int pos, float delay = 5.0f)
    {
        yield return new WaitForSeconds(delay);
        RestoreTile(tile, pos);
    }
    public void RestoreTile(Tile tile, Vector3Int pos)
    {
        tilemap.SetTile(pos, tile);
    }
}
