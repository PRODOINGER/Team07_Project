using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int collisionCount = 0; // ��ֹ����� �浹 Ƚ��
    private const int maxCollisions = 3; // �ִ� �浹 Ƚ�� ����
    public GameObject[] lives; // ���� �̹��� �迭
    public GameObject endPanel; // EndPanel ������Ʈ�� ������ ����

    private void Start()
    {
        // �ִ� ���� �� ��ŭ GameObject �迭 �ʱ�ȭ
        lives = new GameObject[3];
        lives[0] = GameObject.Find("Life1");
        lives[1] = GameObject.Find("Life2");
        lives[2] = GameObject.Find("Life3");
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��ֹ��� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Box"))
        {
            collisionCount++; // �浹�ߴٸ� +1��
            Debug.Log(collisionCount);
            // ���� �̹��� ����
            if (collisionCount <= lives.Length)
            {
                Destroy(lives[collisionCount - 1]); // ���� �̹��� ����
            }
            // �浹 Ƚ���� �ִ�ġ�� �����ϸ� ���� ����
            if (collisionCount >= maxCollisions)
            {
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        
        endPanel.SetActive(true);
        
    }
}
