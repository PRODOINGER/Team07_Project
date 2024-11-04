using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text CurScore;
    public Text CurScoreNum;

    public Button startButton;
    public ScoreManager scoreManager;

    public UIManager uiManager;

    private int collisionCount = 0; // ��ֹ����� �浹 Ƚ��
    private const int maxCollisions = 3; // �ִ� �浹 Ƚ��
    public void Start()
    {
        // ���� ���� �ؽ�Ʈ �����
        CurScore.gameObject.SetActive(false);
        CurScoreNum.gameObject.SetActive(false);


        // ScoreManager�� �����ϸ� �ʱ� ���� UI ������Ʈ
        if (scoreManager != null)
        {
            scoreManager.UpdateScoreUI(); 
        }
    }

    public void StartGame()
    {
        CurScore.gameObject.SetActive(true);
       CurScoreNum.gameObject.SetActive(true);

        // ���� �ʱ�ȭ �� UI ������Ʈ
        if (scoreManager != null)
        {
            scoreManager.AddScore(0); // �ʱ�ȭ �뵵�� 0�� �߰�
        }
        collisionCount = 0;
    }
    public void UpdateCollisionCount()
    {
        collisionCount++; // �浹 Ƚ�� ����
        Debug.Log($"�浹 Ƚ��: {collisionCount}");

        // UI ������Ʈ
        if (uiManager != null)
        {
            uiManager.UpdateLifeImages(collisionCount);
        }

        // �ִ� �浹 Ƚ���� �����ϸ� ���� ���� Ʈ����
        if (collisionCount >= maxCollisions)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // ���� ����
    }

}
