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

    [SerializeField]private TilesManager tilesManager;
    [SerializeField]private PlayersManager playersManager;

    // debuffs
    private PlayerState state = PlayerState.Normal;
    private GameObject otherPlayer;

    private Animator animator;

    #region Components to disable on death
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;
    private GameObject child;
    #endregion
    void Start()
    {
        tilesManager = FindObjectOfType<TilesManager>();
        playersManager = FindObjectOfType<PlayersManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        child = transform.GetChild(0).gameObject;
    }
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
    public void Counter(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("FULL COUNTER");
    }
    void FixedUpdate()
    {
        if (state == PlayerState.Dead) return;
        switch (state)
        {
            case PlayerState.Tickled:
                float randX = UnityEngine.Random.Range(-1, 1);
                float randY = UnityEngine.Random.Range(-1, 1);
                Vector2 rand = new Vector2(randX, randY).normalized;
                float tickledSpeed = 0.3f;
                transform.Translate(tickledSpeed * Time.deltaTime * rand);
                break;
            case PlayerState.Seduced:
                if (otherPlayer == null) return;
                Vector3 direction = (otherPlayer.transform.position - transform.position).normalized;
                float slowSpeed = 0.4f;
                transform.Translate(slowSpeed * Time.deltaTime * direction);
                break;
            case PlayerState.Feared:
                if (otherPlayer == null) return;
                Vector3 fearDir = -(otherPlayer.transform.position - transform.position).normalized;
                float fearSpeed = 0.4f;
                transform.Translate(fearSpeed * Time.deltaTime * fearDir);
                break;
            case PlayerState.Normal:
                transform.Translate(speed * Time.deltaTime * movementInput);
                break;
        }
    }
    private void Update()
    {
        if (state == PlayerState.Dead) return;
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

    public void SetDebuff(PlayerState debuff, GameObject otherPlayer)
    {
        switch (debuff)
        {
            case PlayerState.Tickled:
                DebuffTimer(PlayerState.Tickled, 1.0f, otherPlayer);
                break;
            case PlayerState.Seduced:
                DebuffTimer(PlayerState.Seduced, 1.5f, otherPlayer);
                break;
            case PlayerState.Feared:
                DebuffTimer(PlayerState.Feared, 1.5f, otherPlayer);
                break;
        }
    }
    public void SetState(PlayerState state)
    {
        this.state = state;
        if (state == PlayerState.Dead)
        {
            spriteRenderer.enabled = false;
            collider.enabled = false;
            child.SetActive(false);
        }
        else
        {
            spriteRenderer.enabled = true;
            collider.enabled = true;
            child.SetActive(true);
        }
    }
    public PlayerState GetState()
    {
        return state;
    }
    private async void DebuffTimer(PlayerState debuff, float delay, GameObject otherPlayer)
    {
        state = debuff;
        this.otherPlayer = otherPlayer;
        await Task.Delay((int)delay * 1000);
        state = (state==debuff && state!=PlayerState.Dead)? PlayerState.Normal: debuff;
    }
}
public enum PlayerState
{
    Tickled,
    Seduced,
    Feared,
    Normal,
    Dead
}
