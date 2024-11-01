using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    public ScoreManager scoreManager; // ������ �����ϴ� ScoreManager�� ���� ����

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            scoreManager.AddScore(1); // ���� �߰�
            Debug.Log("������ ������ϴ�!");
            Destroy(gameObject); // ������ ������ ������ �� ����
        }
    }
}