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

    public int collisionCount = 0; // 장애물과의 충돌 횟수
    private const int maxCollisions = 3; // 최대 충돌 횟수

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
            scoreManager.StartScoreUI(); // UI를 활성화하고 점수 초기화
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

        // 점수 초기화 및 UI 업데이트
        if (scoreManager != null)
        {
            scoreManager.StartScoreUI();
        }

        collisionCount = 0;
    }

    public void Update()
    {
        // Bc Scene에서 UIManager 찾기
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public void UpdateCollisionCount()
    {
        collisionCount++; // 충돌 횟수 증가
        Debug.Log($"충돌 횟수: {collisionCount}");

        // UIManager를 통해 생명 이미지 업데이트
        if (uiManager != null)
        {
            uiManager.UpdateLifeImagesOnCollision(); // 생명 이미지 업데이트
        }

        // 최대 충돌 횟수에 도달하면 게임 오버 트리거
        if (collisionCount >= maxCollisions)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // 게임 멈춤

        if (EndPanel != null)
        {
            EndPanel.SetActive(true);
        }
        foreach (Transform child in EndPanel.transform.parent) // EndPanel의 부모인 Canvas의 모든 자식을 순회
        {
            if (child.gameObject != EndPanel) // EndPanel이 아닌 경우
            {
                child.gameObject.SetActive(false); // 비활성화
            }
        }
    }
}