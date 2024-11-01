using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;


public class ScoreManager : MonoBehaviour
{
    public Text curScoreTxt; // 현재 점수 표기
    public int curScore = 0;
    private bool canEarnPoints = true; // 플레이어가 장애물(Box)과 충돌했는지 확인하고, 충돌하면 점수 얻지 못하도록 설정
    

    protected virtual void UpdateScoreUI() // 필요시 Record 에서 추가수정
    {
        curScoreTxt.text = curScore.ToString(); // 현재점수 text 
    }

    private void OnCollisionStay(Collision collision)
    {
        // 임시)-박스 태그가 붙은 오브젝트랑 충돌하면 fasle로 설정.
        canEarnPoints = !collision.gameObject.CompareTag("Box"); 
    }

    protected void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)) && canEarnPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.collider.CompareTag("NoBlink"))
                {
                    AddScore(1);
                }
            }
        }
    }

    public void AddScore(int score) // Record에서 접근 가능
    {
        curScore += score; //현재점수서 +1
        UpdateScoreUI(); // 그리고 스코어UI를 업데이트
    }
}


