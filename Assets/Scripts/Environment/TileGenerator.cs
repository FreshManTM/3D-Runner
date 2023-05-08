using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] tilesPrefab;
    [SerializeField] GameObject defaultTile;

    GameObject tileToSpawn;
    bool isDelay;
    List<GameObject> activeTiles = new List<GameObject>();


    float tileSpawnPos = 0;
    float tileLength = 50;

    [SerializeField] Transform player;
    [SerializeField] int startTiles = 6;
    private void Start()
    {
        tileSpawnPos = 0;
        tileToSpawn = defaultTile;
        StartCoroutine(StartSpawnDelay());
        isDelay = true;
        ObjectPool.Instance.PreLoad(tileToSpawn, 5);
        for (int i = 0; i <= startTiles; i++)
        {
            SpawnTile();
        }
    }

    IEnumerator StartSpawnDelay()
    {
        yield return new WaitForSeconds(4f);
        isDelay = false;
    }
    private void Update()
    {

        if(player.position.z-10 > tileSpawnPos - (startTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }
    void SpawnTile()
    {
        if (isDelay)
            tileToSpawn = defaultTile;
        else
            tileToSpawn = tilesPrefab[Random.Range(0, tilesPrefab.Length)];

        GameObject nextTile =  ObjectPool.Instance.Spawn(tileToSpawn, transform.forward * tileSpawnPos, Quaternion.identity);
        activeTiles.Add(nextTile);
        tileSpawnPos += tileLength;
    }
    void DeleteTile()
    {
        ObjectPool.Instance.Despawn(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
