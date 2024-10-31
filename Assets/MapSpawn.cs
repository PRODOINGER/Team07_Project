using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MapManager mapManager = FindObjectOfType<MapManager>();
            mapManager.RepositionMap();
        }
    }
}
