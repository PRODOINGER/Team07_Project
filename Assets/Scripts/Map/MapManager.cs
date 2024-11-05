using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map")]
    public GameObject[] mapPrefabs;
    public int initialMapCount = 10;
    public float mapLength = 30f;

    [Header("Obstacle")]
    public GameObject[] obstaclePrefab;
    public float minZDistance = 1f;
    public float maxZDistance = 2f;
    public float obstacleNumber;

    [Header("Item")]
    public GameObject[] itemPrefabs;
    public float itemSpawnProbability = 0.3f; // 아이템 생성 확률 (30%)

    [Header("ScoreTrigger")]

    public GameObject scoreTrigger;

    private Queue<GameObject> mapPool = new Queue<GameObject>();
    private Vector3 nextMapPosition = Vector3.zero;

    private void Start()
    {
        InitializeMapPool();
    }

    private void InitializeMapPool()
    {
        for (int i = 0; i < initialMapCount; i++)
        {
            GameObject map = Instantiate(GetRandomMapPrefab(), nextMapPosition, Quaternion.identity);
            mapPool.Enqueue(map);
            nextMapPosition.z += mapLength;
            SpawnObstacleOnMap(map);
        }
    }

    private GameObject GetRandomMapPrefab()
    {
        return mapPrefabs[Random.Range(0, mapPrefabs.Length)];
    }

    private void SpawnObstacleOnMap(GameObject map)
    {
        
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
        float obstacleX = new float[] { 0, 8, -8 }[Random.Range(0, 3)];
        float obstacleZ = nextMapPosition.z + Random.Range(minZDistance, maxZDistance);

        Vector3 obstaclePosition = new Vector3(obstacleX, map.transform.position.y - 6f, obstacleZ);
        Vector3 tiggerPosition = new Vector3(obstacleX, map.transform.position.y, obstacleZ);
        Instantiate(obstaclePrefab[obstacleIndex], obstaclePosition, Quaternion.identity, map.transform);
        Instantiate(scoreTrigger, tiggerPosition, Quaternion.identity, map.transform);
        if (Random.value < itemSpawnProbability)
        {
            int itemIndex = Random.Range(0, itemPrefabs.Length);
            Vector3 itemPosition = new Vector3(obstacleX, map.transform.position.y - 5f, obstacleZ + 15f);
            Instantiate(itemPrefabs[itemIndex], itemPosition, Quaternion.identity, map.transform);
        }
    }

    public void RepositionMap()
    {
        GameObject oldMap = mapPool.Dequeue();
        oldMap.SetActive(false);

        GameObject newMap = Instantiate(GetRandomMapPrefab(), nextMapPosition, Quaternion.identity);
        newMap.SetActive(true);

        SpawnObstacleOnMap(newMap);

        nextMapPosition.z += mapLength;

        Destroy(oldMap);
        mapPool.Enqueue(newMap);
    }
}
