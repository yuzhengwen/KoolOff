using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesManager : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private TileBase crackedTile1;
    [SerializeField] private TileBase crackedTile2;
    [SerializeField] private TileBase normalTile;
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
        Tile tile = tilemap.GetTile<Tile>(pos);
        tilemap.SetTile(pos, crackedTile1);
        tilemap.RefreshTile(pos);
        yield return new WaitForSeconds(delay / 2);
        tilemap.SetTile(pos, crackedTile2);
        tilemap.RefreshTile(pos);

        yield return new WaitForSeconds(delay/2);
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
    IEnumerator RestoreTileCo(Tile tile, Vector3Int pos, float delay = 2.5f)
    {
        yield return new WaitForSeconds(delay);
        RestoreTile(tile, pos);
    }
    public void RestoreTile(Tile tile, Vector3Int pos)
    {
        tilemap.SetTile(pos, normalTile);
        tilemap.RefreshTile(pos);
    }
}
