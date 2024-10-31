using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mapPrefabs;
    [SerializeField]
    private int initialMapCount = 5;

    private int prefabsLenth = 30;
    private float spawnZ = 0f;

    int random = 0;

    public void SpwanMap() 
    {
        random = Random.Range(0, 2);
        GameObject go;
        go = Instantiate(mapPrefabs[random]); 
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += prefabsLenth;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("»ý¼º");
        }

    }

    private void Start()
    {
        for (int i = 0; i < initialMapCount; i++)
        {
            SpwanMap();
        }
        
    }
}
