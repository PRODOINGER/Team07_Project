using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstacle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어 태그가 붙은 오브젝트랑 충돌시 추가
        {
            //Debug.Log("충돌");
            obstacle.SetActive(false);
        }
            
    }
}
