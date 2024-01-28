using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playersManager = transform.parent.GetComponent<PlayersManager>();
        tilesManager = playersManager.tilesManager;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        switch (debuff)
        {
            case PlayerDebuff.Tickled:
                Debug.Log("Tickling");
                float randX = UnityEngine.Random.Range(-1, 1);
                float randY = UnityEngine.Random.Range(-1, 1);
                Vector2 rand = new Vector2(randX, randY).normalized;
                Debug.Log(rand);
                float tickledSpeed = 0.3f;
                transform.Translate(tickledSpeed * Time.deltaTime * rand);
                break;
            case PlayerDebuff.Seduced:
                if (otherPlayer == null) return;
                Vector3 direction = (otherPlayer.transform.position - transform.position).normalized;
                float slowSpeed = 0.4f;
                transform.Translate(slowSpeed * Time.deltaTime * direction);
                break;
            case PlayerDebuff.Feared:
                if (otherPlayer == null) return;
                Vector3 fearDir = -(otherPlayer.transform.position - transform.position).normalized;
                float fearSpeed = 0.4f;
                transform.Translate(fearSpeed * Time.deltaTime * fearDir);
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
        UpdateSprite();

        tilePos = GetTileBelowPlayer();
        if (!tilesManager.tilemap.HasTile(tilePos))
            playersManager.SetPlayerDead(gameObject);
        else
        {
            tilesManager.DestroyTileDelayed(tilePos);
        }
        if (movementInput == Vector2.zero) animator.SetBool("Moving", false);
        else animator.SetBool("Moving", true);
    }

    private void UpdateSprite()
    {
        // TODO
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
                DebuffTimer(PlayerDebuff.Tickled, 1.0f, otherPlayer);
                break;
            case PlayerDebuff.Seduced:
                DebuffTimer(PlayerDebuff.Seduced, 1.5f, otherPlayer);
                break;
            case PlayerDebuff.Feared:
                DebuffTimer(PlayerDebuff.Feared, 1.5f, otherPlayer);
                break;
        }
    }
    private async void DebuffTimer(PlayerDebuff debuff, float delay, GameObject otherPlayer)
    {
        this.debuff = debuff;
        this.otherPlayer = otherPlayer;
        await Task.Delay((int)delay * 1000);
        this.debuff = this.debuff==debuff? PlayerDebuff.None: debuff;
    }
}
public enum PlayerDebuff
{
    Tickled,
    Seduced,
    Feared,
    None
}
