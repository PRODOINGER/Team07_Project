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

        int random = Random.Range(0, obstaclePrefab.Length);
        float obstacleX = new float[] { 0, 8, -8 }[Random.Range(0, 3)];
        float obstacleZ = nextMapPosition.z + Random.Range(minZDistance, maxZDistance);

        Vector3 obstaclePosition = new Vector3(obstacleX, map.transform.position.y - 6, obstacleZ);
        Instantiate(obstaclePrefab[random], obstaclePosition, Quaternion.identity, map.transform);


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
