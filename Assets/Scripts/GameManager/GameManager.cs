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

    private int collisionCount = 0; // 장애물과의 충돌 횟수
    private const int maxCollisions = 3; // 최대 충돌 횟수
    public void Start()
    {
        // 게임 시작 텍스트 숨기기
        CurScore.gameObject.SetActive(false);
        CurScoreNum.gameObject.SetActive(false);


        // ScoreManager가 존재하면 초기 점수 UI 업데이트
        if (scoreManager != null)
        {
            scoreManager.UpdateScoreUI(); 
        }
    }

    public void StartGame()
    {
        CurScore.gameObject.SetActive(true);
       CurScoreNum.gameObject.SetActive(true);

        // 점수 초기화 및 UI 업데이트
        if (scoreManager != null)
        {
            scoreManager.AddScore(0); // 초기화 용도로 0점 추가
        }
        collisionCount = 0;
    }
    public void UpdateCollisionCount()
    {
        collisionCount++; // 충돌 횟수 증가
        Debug.Log($"충돌 횟수: {collisionCount}");

        // UI 업데이트
        if (uiManager != null)
        {
            uiManager.UpdateLifeImages(collisionCount);
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
    }

}
