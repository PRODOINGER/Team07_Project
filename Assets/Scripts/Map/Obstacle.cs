using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstacle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾� �±װ� ���� ������Ʈ�� �浹�� �߰�
        {
            //Debug.Log("�浹");
            obstacle.SetActive(false);
        }
            
    }
}
