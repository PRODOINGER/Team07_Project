using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstacle;

    private void OnCollisionEnter(Collision collision)
    {
        obstacle.SetActive(false);
    }
}
