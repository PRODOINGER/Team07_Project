using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcmManager : MonoBehaviour
{
    public Image achievementImage1; // 첫 번째 업적 아이콘 UI 이미지
    public Image achievementImage2; // 두 번째 업적 아이콘 UI 이미지
    public Image achievementImage3; // 세 번째 업적 아이콘 UI 이미지
    public Sprite acm1Sprite; // 첫 번째 업적 기본 스프라이트
    public Sprite acm1ClearSprite; // 첫 번째 업적 달성 후 스프라이트
    public Sprite acm2Sprite; // 두 번째 업적 기본 스프라이트
    public Sprite acm2ClearSprite; // 두 번째 업적 달성 후 스프라이트
    public Sprite acm3Sprite; // 세 번째 업적 기본 스프라이트
    public Sprite acm3ClearSprite; // 세 번째 업적 달성 후 스프라이트
    public GameObject achievementClearUI; // 업적 클리어 UI
    private bool firstAchievementUnlocked = false;
    private bool secondAchievementUnlocked = false;
    private bool thirdAchievementUnlocked = false;

    private ScoreManager scoreManager; // ScoreManager 참조

    void Start()
    {
        // ScoreManager를 찾거나 연결
        scoreManager = FindObjectOfType<ScoreManager>();

        // 기본 업적 스프라이트 설정
        if (achievementImage1 != null)
            achievementImage1.sprite = acm1Sprite;
        if (achievementImage2 != null)
            achievementImage2.sprite = acm2Sprite;
        if (achievementImage3 != null)
            achievementImage3.sprite = acm3Sprite;

        // 업적 클리어 UI 초기 비활성화
        if (achievementClearUI != null)
            achievementClearUI.SetActive(false);
    }

    void Update()
    {
        // 첫 번째 업적: 스페이스바로 점프
        if (Input.GetKeyDown(KeyCode.Space) && !firstAchievementUnlocked)
            UnlockFirstAchievement();

        // 두 번째 업적: 쉬프트키로 구르기
        if (Input.GetKeyDown(KeyCode.LeftShift) && !secondAchievementUnlocked)
            UnlockSecondAchievement();

        // 세 번째 업적: 점수가 100 이상일 때
        if (scoreManager != null && scoreManager.CurScoreNumValue >= 100 && !thirdAchievementUnlocked)
            UnlockThirdAchievement();
    }

    void UnlockFirstAchievement()//첫번째 업적 클리어
    {
        firstAchievementUnlocked = true;
        if (achievementImage1 != null)
        {
        achievementImage1.sprite = acm1ClearSprite;
        }
        Debug.Log("첫번째 업적 달성!");
        StartCoroutine(ShowAchievementClearUI());
    }

    void UnlockSecondAchievement()//두번째 업적 클리어
    {
        secondAchievementUnlocked = true;
        if (achievementImage2 != null)
        {
        achievementImage2.sprite = acm2ClearSprite;
        }
        Debug.Log("두번째 업적 달성!");
        StartCoroutine(ShowAchievementClearUI());
    }

    void UnlockThirdAchievement()//세번째 업적 클리어
    {
        thirdAchievementUnlocked = true;
        if (achievementImage3 != null)
        {
        achievementImage3.sprite = acm3ClearSprite;
        }
        Debug.Log("세번째 업적 달성!");
        StartCoroutine(ShowAchievementClearUI());
    }

    IEnumerator ShowAchievementClearUI()
    {
        if (achievementClearUI != null)
        {
            achievementClearUI.SetActive(true); // 업적 클리어 UI 활성화
            yield return new WaitForSeconds(0.7f); // 0.7초 대기
            achievementClearUI.SetActive(false); // 업적 클리어 UI 비활성화
        }
    }
}
