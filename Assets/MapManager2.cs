using System.Collections.Generic;
using UnityEngine;

public class MapManager2 : MonoBehaviour
{
    public GameObject[] mapPrefabs; 
    public int initialMapCount = 5; 
    public float mapLength = 30f; 
    public Transform player; 

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
        }
    }

    private GameObject GetRandomMapPrefab()
    {
        return mapPrefabs[Random.Range(0, mapPrefabs.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RepositionMap();
        }
    }

    private void RepositionMap()
    {
        GameObject oldMap = mapPool.Dequeue();
        oldMap.SetActive(false);

        GameObject newMap = Instantiate(GetRandomMapPrefab(), nextMapPosition, Quaternion.identity);
        newMap.SetActive(true);

        nextMapPosition.z += mapLength;

        Destroy(oldMap);
        mapPool.Enqueue(newMap);
    }
}
