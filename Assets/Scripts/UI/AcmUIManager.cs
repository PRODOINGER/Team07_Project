using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcmUIManager : MonoBehaviour
{
    public GameObject achievementPanel;

    // ���� �г��� Ȱ��ȭ��Ű�� �Լ�
    public void ToggleAchievementUI()
    {
        if (achievementPanel != null)
        {
            bool isActive = achievementPanel.activeSelf;
            // �г��� ���� ���¿� ���� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ ��ȯ
            achievementPanel.SetActive(!achievementPanel.activeSelf);

            // UI Ȱ��ȭ �� ������ �Ͻ� �����ϰ�, ��Ȱ��ȭ �� �簳
            Time.timeScale = isActive ? 1 : 0;
        }
    }
}
