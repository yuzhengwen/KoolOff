using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    private Vector3Int respawnPos = new Vector3Int(0, 0, 0);
    [SerializeField] public TilesManager tilesManager;
    [SerializeField] public GameObject ui_inventory;

    [SerializeField] private GameObject playerPrefab;
    private void Awake()
    {
        SpawnPlayers();
    }
    private void SpawnPlayers()
    {
        PlayerInput[] players = new PlayerInput[2];
        players[0] = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
        players[0].defaultControlScheme = "Keyboard1";
        players[1] = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        players[1].defaultControlScheme = "Keyboard2";
        for (int i = 0; i < players.Length; i++)
        {
            players[i]?.transform.SetParent(transform);
            ui_inventory.GetComponent<UI_Inventory>().AddPlayer(players[i]);
        }
    }

    public void SetPlayerDead(GameObject player)
    {
        //player.SetActive(false);
        player.GetComponent<PlayerMovement>().SetState(PlayerState.Dead);
        StartCoroutine(RespawnPlayerCo(player));
    }
    IEnumerator RespawnPlayerCo(GameObject player, float delay = 3.0f)
    {
        yield return new WaitForSeconds(delay);

        player.transform.position = tilesManager.tilemap.CellToWorld(tilesManager.getRandomTileLocation());
        //player.SetActive(true);
        player.GetComponent<PlayerMovement>().SetState(PlayerState.Normal);
        //string control= player.GetComponent<PlayerInput>().currentControlScheme;
        //player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(control, Keyboard.current);
    }
}
