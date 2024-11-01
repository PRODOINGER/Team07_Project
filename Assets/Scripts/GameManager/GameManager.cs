using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        // ScoreManager 컴포넌트 가져오기
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager를 찾을 수 없습니다.");
        }
    }

    public void AddScore(int points)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }
    }

}
