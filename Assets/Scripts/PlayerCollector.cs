using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private Inventory inventory;
    private void Start()
    {
        inventory = GetComponent<Inventory>();
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
    private void AddItemToInventory(ICollectible item)
    {
        inventory.AddItem(item);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.OnCollect();
        }
    }
}
