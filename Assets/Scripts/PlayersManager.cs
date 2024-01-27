using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayersManager : MonoBehaviour
{
    private Vector3Int respawnPos = new Vector3Int(0, 0, 0);
    [SerializeField] public TilesManager tilesManager;
    [SerializeField] public UI_Inventory uiInventory;

    [SerializeField] private GameObject playerPrefab;
    private void Awake()
    {
        SpawnPlayers();
    }
    private void SpawnPlayers()
    {
        var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        var p3 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard3", pairWithDevice: Keyboard.current);
        p1.transform.SetParent(transform);
        p2.transform.SetParent(transform);
        p3.transform.SetParent(transform);
    }
    public void SetPlayerDead(GameObject player)
    {
        player.SetActive(false);
        StartCoroutine(RespawnPlayerCo(player));
    }
    IEnumerator RespawnPlayerCo(GameObject player, float delay = 3.0f)
    {
        yield return new WaitForSeconds(delay);

        player.transform.position = tilesManager.tilemap.CellToWorld(tilesManager.getRandomTileLocation());
        player.SetActive(true);
    }
}
