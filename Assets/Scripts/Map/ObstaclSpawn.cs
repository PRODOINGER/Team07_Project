using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclSpawn : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    public float minZDistance = 5f;     
    public float maxZDistance = 10f;     
    public int obstacleCount = 3;

    public void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            float obstacleX = new float[] { 0, 8, -8 }[Random.Range(0, 3)];

            float obstacleZ = transform.position.z + Random.Range(minZDistance, maxZDistance);

            Vector3 obstaclePosition = new Vector3(obstacleX, transform.position.y, obstacleZ);

            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(obstacle, obstaclePosition, Quaternion.identity, transform);
        }
    }
}
