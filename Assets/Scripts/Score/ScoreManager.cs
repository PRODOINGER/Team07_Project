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

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 패턴 적용: 인스턴스가 없다면 현재 인스턴스를 사용하고 중복된 경우 파괴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 후에도 오브젝트 유지 // 부모객체가 있으면 안됨
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재할 경우 중복 제거
            return;
        }
    }
    private void Start()
    {
        StartScoreUI();
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
        
        if (other.CompareTag("Score"))
        {
            AddScore(1);           
            Debug.Log("점수를 얻었습니다!");
            Destroy(other.gameObject); 
        }
    }

    public void AddScore(int score)
    {
        CurScoreNumValue += score; // 현재 점수에 추가
        UpdateScoreUI(); // 스코어 UI를 업데이트
        CheckAndSetHighScore();
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

