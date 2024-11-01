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

    void Start()
    {
        // 게임 시작 텍스트 숨기기
        CurScore.gameObject.SetActive(false);
        CurScoreNum.gameObject.SetActive(false);

        // 버튼에 StartGame 메서드 연결
        startButton.onClick.AddListener(StartGame);

        // ScoreManager가 존재하면 초기 점수 UI 업데이트
        if (scoreManager != null)
        {
            scoreManager.UpdateScoreUI(); 
        }
    }

    void StartGame()
    {
        CurScore.gameObject.SetActive(true);
       CurScoreNum.gameObject.SetActive(true);

        // 점수 초기화 및 UI 업데이트
        if (scoreManager != null)
        {
            scoreManager.AddScore(0); // 초기화 용도로 0점 추가
        }
    }

}
