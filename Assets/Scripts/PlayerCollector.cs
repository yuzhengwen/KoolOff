using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollector : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private GameObject inventoryPrefab;
    private void Awake()
    {
        GameObject invObj = Instantiate(inventoryPrefab);
        invObj.transform.parent = transform;
        inventory = invObj.GetComponent<Inventory>();
    }
    public void OnEnable()
    {
        Feather.OnCollected += AddItemToInventory;
        Heart.OnCollected += AddItemToInventory;
    }

    public void OnDisable()
    {
        Feather.OnCollected-= AddItemToInventory;
        Heart.OnCollected -= AddItemToInventory;
    }
    public void UseItem(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        inventory.UseItem(gameObject);
    }
    private void AddItemToInventory(ICollectible item)
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
