using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreNum;
    public TextMeshProUGUI CurScoreText;
    public TextMeshProUGUI CurScoreNum;
    public int CurScoreNumValue = 0;
    private int highScore = 0;

    private void Start()
    {
        StartScoreUI();
        LoadHighScore(); // 기존 최고 점수 불러오기

        // 최고 점수 초기 표시
        if (HighScoreNum != null)
        {
            HighScoreNum.text = highScore.ToString();
        }

        InvokeRepeating("AddScoreEverySecond", 1f, 1f);
    }

    // 점수 UI를 초기화하고 활성화하는 메서드
    public void StartScoreUI()
    {
        if (CurScoreText != null)
        {
            CurScoreText.gameObject.SetActive(true);
        }
        if (CurScoreNum != null)
        {
            CurScoreNum.gameObject.SetActive(true);
        }

        // 점수 초기화 및 UI 업데이트
        CurScoreNumValue = 0;
        UpdateScoreUI();
    }

    public virtual void UpdateScoreUI()
    {
        if (CurScoreNum != null)
        {
            CurScoreNum.text = CurScoreNumValue.ToString();
        }
    }

    // 게임 시간에 따라 1점씩 추가하는 메서드
    private void AddScoreEverySecond()
    {
        AddScore(1); // 1초마다 점수 1점씩 증가
    }

    public void AddScore(int score)
    {
        CurScoreNumValue += score; // 현재 점수에 추가
        UpdateScoreUI(); // 스코어 UI를 업데이트
        CheckAndSetHighScore();
        Debug.Log("점수획득");
    }

    private void CheckAndSetHighScore()
    {
        if (CurScoreNumValue > highScore)
        {
            highScore = CurScoreNumValue;
            SaveHighScore();

            // 최고 점수 UI 숫자 업데이트
            if (HighScoreNum != null)
            {
                HighScoreNum.text = highScore.ToString();
            }
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
