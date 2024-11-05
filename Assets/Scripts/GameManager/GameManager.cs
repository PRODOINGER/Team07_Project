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

    private int collisionCount = 0; // 장애물과의 충돌 횟수
    private const int maxCollisions = 3; // 최대 충돌 횟수

    public GameObject Life;
    public GameObject EndPanel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 패턴 적용: 인스턴스가 없다면 현재 인스턴스를 사용하고 중복된 경우 파괴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 후에도 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재할 경우 중복 제거
            return;
        }
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
            if (uiManager == null)
            {
                Debug.LogWarning("UIManager를 찾을 수 없습니다. Bc Scene에 UIManager가 있는지 확인하세요.");
            }
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
    }
}