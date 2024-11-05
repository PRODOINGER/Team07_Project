using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI CurScoreText;
    public TextMeshProUGUI CurScoreNum;

    public Button startButton;
    public ScoreManager scoreManager;

    public UIManager uiManager;

    private int collisionCount = 0; // ��ֹ����� �浹 Ƚ��
    private const int maxCollisions = 3; // �ִ� �浹 Ƚ��

    public GameObject Life;
    public GameObject EndPanel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // �̱��� ���� ����: �ν��Ͻ��� ���ٸ� ���� �ν��Ͻ��� ����ϰ� �ߺ��� ��� �ı�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �Ŀ��� ������Ʈ ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ��� �ߺ� ����
            return;
        }
    }
    public void Start()
    {
        if (scoreManager != null)
        {
            scoreManager.StartScoreUI(); // UI�� Ȱ��ȭ�ϰ� ���� �ʱ�ȭ
        }
        collisionCount = 0;

        if (Life != null)
        {
            Life.SetActive(true);
        }

            if (EndPanel != null)
        {
            EndPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        CurScoreText.gameObject.SetActive(true);
        CurScoreNum.gameObject.SetActive(true);

        // ���� �ʱ�ȭ �� UI ������Ʈ
        if (scoreManager != null)
        {
            scoreManager.StartScoreUI();
            
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
      
        if (EndPanel != null)
        {
            EndPanel.SetActive(true);
        }
    }

}
