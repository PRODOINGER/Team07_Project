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

    public ScoreManager scoreManager;

    public UIManager uiManager;

    public int collisionCount = 0; // ��ֹ����� �浹 Ƚ��
    private const int maxCollisions = 3; // �ִ� �浹 Ƚ��

    public GameObject Life;
    public GameObject EndPanel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
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

    public void Update()
    {
        // Bc Scene���� UIManager ã��
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public void UpdateCollisionCount()
    {
        collisionCount++; // �浹 Ƚ�� ����
        Debug.Log($"�浹 Ƚ��: {collisionCount}");

        // UIManager�� ���� ���� �̹��� ������Ʈ
        if (uiManager != null)
        {
            uiManager.UpdateLifeImagesOnCollision(); // ���� �̹��� ������Ʈ
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
        foreach (Transform child in EndPanel.transform.parent) // EndPanel�� �θ��� Canvas�� ��� �ڽ��� ��ȸ
        {
            if (child.gameObject != EndPanel) // EndPanel�� �ƴ� ���
            {
                child.gameObject.SetActive(false); // ��Ȱ��ȭ
            }
        }
    }
}