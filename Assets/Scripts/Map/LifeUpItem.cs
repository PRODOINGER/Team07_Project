using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LifeUpItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager gameManager = GetComponent<GameManager>();
        UIManager uIManager = GetComponent<UIManager>();
        //gameManager.collisionCount -= 1;
    }
}
