using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int collisionCount = 0; // ��ֹ����� �浹 Ƚ��
    private const int maxCollisions = 3; // �ִ� �浹 Ƚ�� ����

    private void OnTriggerEnter(Collider other)
    {
        // ��ֹ��� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Box"))
        {
            collisionCount++; // �浹�ߴٸ� +1��

            // �浹 Ƚ���� �ִ�ġ�� �����ϸ� ���� ����
            if (collisionCount >= maxCollisions)
            {
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        // Retry ȭ������ �̵�
        SceneManager.LoadScene("RetryScene");
    }
}
