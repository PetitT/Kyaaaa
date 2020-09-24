using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnedItems;
    public List<Transform> spawnPoints;

    public float baseSpawnTime;
    public float spawnTimeBuff;
    public float minSpawnTime;
    public float spawnTimeBuffTimer;

    public float baseItemSpeed;
    public float itemSpeedBuff;
    public float maxItemSpeed;
    public float itemSpeedBuffTimer;

    private float currentSpawnTime;
    private float currentItemSpeed;

    private void Start()
    {
        currentItemSpeed = baseItemSpeed;
        currentSpawnTime = baseSpawnTime;
        StartCoroutine(SpawnTime());
        StartCoroutine(Buff());
        StartCoroutine(BuffItems());
    }

    private IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(currentSpawnTime);
        SpawnItem();
        StartCoroutine(SpawnTime());
    }

    private IEnumerator Buff()
    {
        yield return new WaitForSeconds(spawnTimeBuffTimer);
        currentSpawnTime -= spawnTimeBuff;
        

        if(currentSpawnTime > minSpawnTime)
        {
            StartCoroutine(Buff());
        }
    }

    private IEnumerator BuffItems()
    {
        yield return new WaitForSeconds(itemSpeedBuffTimer);
        currentItemSpeed += itemSpeedBuff;
        if(currentItemSpeed < maxItemSpeed)
        {
            StartCoroutine(BuffItems());
        }
    }

    private void SpawnItem()
    {
        int spawnPosIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        int itemIndex = UnityEngine.Random.Range(0, spawnedItems.Count);
        Vector2 spawnPos = spawnPoints[spawnPosIndex].position;
        GameObject spawnedItem = spawnedItems[itemIndex];

        GameObject newItem = Instantiate(spawnedItem, spawnPos, Quaternion.identity);

        if(newItem.TryGetComponent(out Coin coin))
        {
            coin.speed = currentItemSpeed;
        }
        if(newItem.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.speed = currentItemSpeed;
        }
    }
}
