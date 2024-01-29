using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollector : MonoBehaviour
{
    private Inventory inventory;
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    public void UseItem(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (GetComponent<PlayerMovement>().GetState() == PlayerState.Normal) 
            inventory.UseItem(gameObject);
    }
    public void AddItemToInventory(ICollectible item)
    {
        inventory.AddItem(item);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.OnCollect(gameObject);
        }
    }
}
