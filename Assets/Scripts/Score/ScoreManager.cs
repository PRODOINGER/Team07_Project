using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI CurScoreText;
    public TextMeshProUGUI CurScoreNum; 
    public int CurScoreNumValue = 0;
    private int highScore = 0;


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
        CurScoreNumValue = 0; // 초기화 용도로 0점 추가
        UpdateScoreUI(); // 초기 점수 표시
    }

    public virtual void UpdateScoreUI()
    {
        if (CurScoreNum != null)
        {
            CurScoreNum.text = CurScoreNumValue.ToString(); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌했는지 확인
        if (other.CompareTag("Score"))
        {
            AddScore(1);           // 점수 추가
            Debug.Log("점수를 얻었습니다!");
            Destroy(other.gameObject); // 점수를 얻으면 충돌한 오브젝트 삭제
        }
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
        if (CurScoreNumValue > highScore) // 만약 현재 점수가 최고 점수보다 높다면
        {
            highScore = CurScoreNumValue; // 최고 점수를 현재 점수로 업데이트
            SaveHighScore(); // 그리고 저장
        }
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); // 최고 점수를 저장
        PlayerPrefs.Save(); // PlayerPrefs 저장
    }
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // 저장된 최고 점수 로드 (기본값 0)
    }


    
}

