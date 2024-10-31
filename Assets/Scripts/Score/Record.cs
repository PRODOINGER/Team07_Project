using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Record : MonoBehaviour
{
    private int highScore = 0; // 맨 처음 시작할때 값 = 0
    public int curScore = 0; // 현재점수도 0으로 시작. 

    // 현재점수와 최고점수 가져오기
    public (int highScore, int curScore) GetScores()
    {
        return (highScore, curScore);
    }

    // 현재점수랑 비교해서 최고점수보다 높으면 최고점수로 저장하기
    public void CheckAndSetHighScore(int currentScore)
    {
        curScore = currentScore; // 현재점수 업데이트

        if (curScore > highScore) // 만약 현재점수가 최고점수보다 높다면
        {
            highScore = curScore; // 최고점수를 현재점수로 
            SaveHighScore(); // 그리고 저장
        }
    }

    // 저장된 최고점수를 프리팹화 하여 저장하기
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); // 최고점수 값을 최고점수로 지정
        PlayerPrefs.Save(); // 그리고 저장
    }

    //필요할때 로드
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
