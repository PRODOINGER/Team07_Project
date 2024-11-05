using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcmUIManager : MonoBehaviour
{
    public GameObject achievementPanel;

    // 업적 패널을 활성화시키는 함수
    public void ToggleAchievementUI()
    {
        if (achievementPanel != null)
        {
            bool isActive = achievementPanel.activeSelf;
            // 패널의 현재 상태에 따라 활성화 또는 비활성화 전환
            achievementPanel.SetActive(!achievementPanel.activeSelf);

            // UI 활성화 시 게임을 일시 정지하고, 비활성화 시 재개
            Time.timeScale = isActive ? 1 : 0;
        }
    }
}
