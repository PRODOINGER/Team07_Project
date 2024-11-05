using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcmManager : MonoBehaviour
{
    public Image achievementImage1; // ù ��° ���� ������ UI �̹���
    public Image achievementImage2; // �� ��° ���� ������ UI �̹���
    public Image achievementImage3; // �� ��° ���� ������ UI �̹���
    public Sprite acm1Sprite; // ù ��° ���� �⺻ ��������Ʈ
    public Sprite acm1ClearSprite; // ù ��° ���� �޼� �� ��������Ʈ
    public Sprite acm2Sprite; // �� ��° ���� �⺻ ��������Ʈ
    public Sprite acm2ClearSprite; // �� ��° ���� �޼� �� ��������Ʈ
    public Sprite acm3Sprite; // �� ��° ���� �⺻ ��������Ʈ
    public Sprite acm3ClearSprite; // �� ��° ���� �޼� �� ��������Ʈ
    public GameObject achievementClearUI; // ���� Ŭ���� UI
    private bool firstAchievementUnlocked = false;
    private bool secondAchievementUnlocked = false;
    private bool thirdAchievementUnlocked = false;

    private ScoreManager scoreManager; // ScoreManager ����

    void Start()
    {
        // ScoreManager�� ã�ų� ����
        scoreManager = FindObjectOfType<ScoreManager>();

        // �⺻ ���� ��������Ʈ ����
        if (achievementImage1 != null)
            achievementImage1.sprite = acm1Sprite;
        if (achievementImage2 != null)
            achievementImage2.sprite = acm2Sprite;
        if (achievementImage3 != null)
            achievementImage3.sprite = acm3Sprite;

        // ���� Ŭ���� UI �ʱ� ��Ȱ��ȭ
        if (achievementClearUI != null)
            achievementClearUI.SetActive(false);
    }

    void Update()
    {
        // ù ��° ����: �����̽��ٷ� ����
        if (Input.GetKeyDown(KeyCode.Space) && !firstAchievementUnlocked)
            UnlockFirstAchievement();

        // �� ��° ����: ����ƮŰ�� ������
        if (Input.GetKeyDown(KeyCode.LeftShift) && !secondAchievementUnlocked)
            UnlockSecondAchievement();

        // �� ��° ����: ������ 100 �̻��� ��
        if (scoreManager != null && scoreManager.CurScoreNumValue >= 100 && !thirdAchievementUnlocked)
            UnlockThirdAchievement();
    }

    void UnlockFirstAchievement()//ù��° ���� Ŭ����
    {
        firstAchievementUnlocked = true;
        if (achievementImage1 != null)
        {
        achievementImage1.sprite = acm1ClearSprite;
        }
        Debug.Log("ù��° ���� �޼�!");
        StartCoroutine(ShowAchievementClearUI());
    }

    void UnlockSecondAchievement()//�ι�° ���� Ŭ����
    {
        secondAchievementUnlocked = true;
        if (achievementImage2 != null)
        {
        achievementImage2.sprite = acm2ClearSprite;
        }
        Debug.Log("�ι�° ���� �޼�!");
        StartCoroutine(ShowAchievementClearUI());
    }

    void UnlockThirdAchievement()//����° ���� Ŭ����
    {
        thirdAchievementUnlocked = true;
        if (achievementImage3 != null)
        {
        achievementImage3.sprite = acm3ClearSprite;
        }
        Debug.Log("����° ���� �޼�!");
        StartCoroutine(ShowAchievementClearUI());
    }

    IEnumerator ShowAchievementClearUI()
    {
        if (achievementClearUI != null)
        {
            achievementClearUI.SetActive(true); // ���� Ŭ���� UI Ȱ��ȭ
            yield return new WaitForSeconds(0.7f); // 0.7�� ���
            achievementClearUI.SetActive(false); // ���� Ŭ���� UI ��Ȱ��ȭ
        }
    }
}
