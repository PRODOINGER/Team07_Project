using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    public ScoreManager scoreManager; // 점수를 관리하는 ScoreManager에 대한 참조

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            scoreManager.AddScore(1); // 점수 추가
            Debug.Log("점수를 얻었습니다!");
            Destroy(gameObject); // 점수를 얻으면 가상의 점 삭제
        }
    }
}