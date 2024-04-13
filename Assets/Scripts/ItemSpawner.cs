using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Vector3 spawnAreaTopLeft;
    public Vector3 spawnAreaBottomRight;

    public void SpawnItem()
    {
        if (itemPrefabs.Length == 0)
        {
            return;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaTopLeft.x, spawnAreaBottomRight.x),
            Random.Range(spawnAreaTopLeft.y, spawnAreaBottomRight.y),
            spawnAreaTopLeft.z
        );

        GameObject randomPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        GameObject spawnedItem = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        ItemController item = spawnedItem.GetComponent<ItemController>();
        // ChatGPT - Formula
        item.maxCollectionBonus = Mathf.Pow(GameManager.instance.scoreLevel, 2) * 100f;
    }
}
