using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public GameObject[] lives;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 패턴 적용: 인스턴스가 없다면 현재 인스턴스를 사용하고 중복된 경우 파괴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 후에도 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재할 경우 중복 제거
            return;
        }
    }
    // 충돌에 따라 이미지 업데이트
    public void UpdateLifeImages(int collisionCount)
    {
        if (collisionCount > 0 && collisionCount <= lives.Length)
        {
            lives[collisionCount - 1].SetActive(false); // 해당 이미지 비활성화
        }
    }
}
