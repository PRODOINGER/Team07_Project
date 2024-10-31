using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int collisionCount = 0; // 장애물과의 충돌 횟수
    private const int maxCollisions = 3; // 최대 충돌 횟수 설정

    private void OnTriggerEnter(Collider other)
    {
        // 장애물과 충돌했는지 확인
        if (other.CompareTag("Box"))
        {
            collisionCount++; // 충돌했다면 +1씩

            // 충돌 횟수가 최대치에 도달하면 게임 종료
            if (collisionCount >= maxCollisions)
            {
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        // Retry 화면으로 이동
        SceneManager.LoadScene("RetryScene");
    }
}
