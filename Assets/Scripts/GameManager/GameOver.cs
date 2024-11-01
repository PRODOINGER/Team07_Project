using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int collisionCount = 0; // 장애물과의 충돌 횟수
    private const int maxCollisions = 3; // 최대 충돌 횟수 설정
    public GameObject[] lives; // 생명 이미지 배열
    public GameObject endPanel; // EndPanel 오브젝트를 참조할 변수

    private void Start()
    {
        // 최대 생명 수 만큼 GameObject 배열 초기화
        lives = new GameObject[3];
        lives[0] = GameObject.Find("Life1");
        lives[1] = GameObject.Find("Life2");
        lives[2] = GameObject.Find("Life3");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 장애물과 충돌했는지 확인
        if (other.CompareTag("Box"))
        {
            collisionCount++; // 충돌했다면 +1씩
            Debug.Log(collisionCount);
            // 생명 이미지 제거
            if (collisionCount <= lives.Length)
            {
                Destroy(lives[collisionCount - 1]); // 생명 이미지 제거
            }
            // 충돌 횟수가 최대치에 도달하면 게임 종료
            if (collisionCount >= maxCollisions)
            {
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        
        endPanel.SetActive(true);
        
    }
}
