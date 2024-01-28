using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> itemPrefabs;
    [SerializeField] TilesManager tilesManager;
    [SerializeField] private int initialItems = 5;
    [SerializeField] private int maxItems = 10;
    [SerializeField] private float spawnDelay = 3.0f;

    void Start()
    {
        for (int i = 0; i < initialItems; i++)
        {
            SpawnAtRandomPos();
        }
        StartCoroutine(SpawnAtIntervalsCo());
    }

    private IEnumerator SpawnAtIntervalsCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if (transform.childCount < maxItems)
            {
                SpawnAtRandomPos();
            }
        }
    }

    private void SpawnAtRandomPos()
    {
        // get random tile location without item already existing
        Vector3 spawnPos;
        do
            spawnPos = tilesManager.tilemap.CellToWorld(tilesManager.getRandomTileLocation());
        while (ItemExist(spawnPos));

        // get random item prefab from list
        int randomIndex = UnityEngine.Random.Range(0, itemPrefabs.Count);
        GameObject obj = Instantiate(itemPrefabs[randomIndex], spawnPos, Quaternion.identity);
        obj.transform.SetParent(transform);
    }
    private bool ItemExist(Vector3 pos)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.down, 10.0f, LayerMask.GetMask("Tiles"));
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<ICollectible>() != null)
            {
                return true;
            }
        }
        return false;
    }
}
