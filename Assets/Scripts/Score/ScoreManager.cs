using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreManager : MonoBehaviour
{
    public Text curScoreTxt; // 현재 점수 표기
    public int curScore = 0;

    public virtual void UpdateScoreUI() // 필요시 Record 에서 추가수정
    {
        curScoreTxt.text = curScore.ToString(); // 현재점수 text 
    }

    protected void Update()
    {
        
    }

    public void AddScore(int score) // Record에서 접근 가능
    {
        curScore += score; //현재점수서 +1
        UpdateScoreUI(); // 그리고 스코어UI를 업데이트
    }
}

