using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;


public class ScoreManager : MonoBehaviour
{
    public Text curScoreTxt; // 현재 점수 표기
    protected int curScore = 0;
    protected bool canEarnPoints = true; // 플레이어가 장애물(Box)과 충돌했는지 확인하고, 충돌하면 점수 얻지 못하도록 설정

    protected virtual void UpdateScoreUI() // 필요시 Record 에서 추가수정
    {
        curScoreTxt.text = curScore.ToString(); // 현재점수 text 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 임시)-박스 태그가 붙은 오브젝트랑 충돌하면 fasle로 설정.
        canEarnPoints = !collision.gameObject.CompareTag("Box");
    }

    protected void Update()
    {
        // Spacebar가 눌린 경우 canEarnPoints를 확인
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canEarnPoints)
        {
            AddScore(1); // true 작동시 +1점 매서드 작동
        }
    }

    protected void AddScore(int score) // Record에서 접근 가능
    {
        curScore += score; //현재점수서 +1
        UpdateScoreUI(); // 그리고 스코어UI를 업데이트
    }
}

