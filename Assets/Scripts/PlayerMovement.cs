using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private float speed = 2;
    private Vector3Int tilePos;

    private TilesManager tilesManager;
    private PlayersManager playersManager;

    // debuffs
    private PlayerDebuff debuff = PlayerDebuff.None;
    private GameObject otherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playersManager = transform.parent.GetComponent<PlayersManager>();
        tilesManager = playersManager.tilesManager;
    }

    void FixedUpdate()
    {
        switch (debuff)
        {
            case PlayerDebuff.Tickled:
                rb.velocity = Vector2.zero;
                break;
            case PlayerDebuff.Seduced:
                rb.velocity = Vector2.zero;
                Vector3 direction = (otherPlayer.transform.position - transform.position).normalized;
                float slowSpeed = 0.5f;
                rb.velocity = direction * slowSpeed;
                break;
            case PlayerDebuff.Pushed:
                rb.velocity = Vector2.zero;
                break;
            case PlayerDebuff.None:
                transform.Translate(speed * Time.deltaTime * movementInput);
                break;
        }
    }
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
    public void Counter(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("FULL COUNTER");
    }
    private void Update()
    {
        tilePos = GetTileBelowPlayer();
        if (!tilesManager.tilemap.HasTile(tilePos))
            playersManager.SetPlayerDead(gameObject);
        else
        {
            //tilesManager.DestroyTileDelayed(tilePos);
        }
    }

    public LayerMask terrain;
    private Vector3Int GetTileBelowPlayer()
    {
        return tilesManager.tilemap.WorldToCell(rb.position);
    }

    public void SetDebuff(PlayerDebuff debuff, GameObject otherPlayer)
    {
        switch (debuff)
        {
            case PlayerDebuff.Tickled:
                //StartCoroutine(TickledCo(otherPlayer));
                Debug.Log("Tickled");
                break;
            case PlayerDebuff.Seduced:
                StartCoroutine(SeducedCo(otherPlayer));
                Debug.Log("Seduced");
                break;
            case PlayerDebuff.Pushed:
                //StartCoroutine(PushedCo(otherPlayer));
                break;
        }
    }

    private IEnumerator SeducedCo(GameObject otherPlayer)
    {
        debuff = PlayerDebuff.Seduced;
        this.otherPlayer = otherPlayer;
        yield return new WaitForSeconds(1.5f);
        debuff = PlayerDebuff.None;
    }
}
public enum PlayerDebuff
{
    Tickled,
    Seduced,
    Pushed,
    None
}
