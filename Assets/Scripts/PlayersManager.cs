using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Tilemaps;

public class PlayersManager : MonoBehaviour
{
    private Vector3Int respawnPos = new Vector3Int(0, 0, 0);
    [SerializeField] public TilesManager tilesManager;
    [SerializeField] public UI_Inventory uiInventory;

    [SerializeField] private GameObject playerPrefab;
    private PlayerInputManager manager;
    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        SpawnPlayers();
    }
    private void SpawnPlayers()
    {
        PlayerInput[] players = new PlayerInput[2];
        players[0] = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
        players[0].defaultControlScheme = "Keyboard1";
        players[1] = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        players[1].defaultControlScheme = "Keyboard2";
        //players[2] = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard3", pairWithDevice: Keyboard.current);
        for (int i = 0; i < players.Length; i++)
        {
            players[i]?.transform.SetParent(transform);
        }

    }

    public void ConfigurePlayer(PlayerInput input)
    {
        input.transform.SetParent(transform);
        Debug.Log("player joined");
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
