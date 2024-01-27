using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayersManager : MonoBehaviour
{
    private Vector3Int respawnPos = new Vector3Int(0, 0, 0);
    [SerializeField] private TilesManager tilesManager;
    public void SetPlayerDead(GameObject player)
    {
        player.SetActive(false);
        StartCoroutine(RespawnPlayerCo(player));
    }

    public List<Vector3Int> tileWorldLocations;
    IEnumerator RespawnPlayerCo(GameObject player, float delay = 3.0f)
    {
        yield return new WaitForSeconds(delay);

        player.transform.position = tilesManager.tilemap.CellToWorld(getRandomTileLocation());
        player.SetActive(true);
    }
    private Vector3Int getRandomTileLocation()
    {
        tileWorldLocations = new List<Vector3Int>();
        foreach (var pos in tilesManager.tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilesManager.tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(localPlace);
            }
        }
        int i = UnityEngine.Random.Range(0, tileWorldLocations.Count);
        return tileWorldLocations[i];
    }
}