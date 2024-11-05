using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives; // Life 오브젝트 배열
    public Sprite mineHeart1; // 충돌 시 변경할 스프라이트
    public static UIManager Instance { get; private set; }
    private int lifeIndex; // 현재 변경할 Life 인덱스

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lifeIndex = lives.Length - 1;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BC Scene")
        {
            // BC Scene에서 "Life" 태그로 모든 오브젝트 찾기
            lives = GameObject.FindGameObjectsWithTag("Life");
            lifeIndex = lives.Length - 1; // lifeIndex 초기화
        }
    }

    public void UpdateLifeImages(int collisionCount)
    {
        if (lifeIndex >= 0 && lifeIndex < lives.Length)
        {
            Image lifeImage = lives[lifeIndex].GetComponent<Image>();
            if (lifeImage != null)
            {
                lifeImage.sprite = mineHeart1;
                lifeIndex--;
            }
            else
            {
                Debug.LogError($"Image component가 {lives[lifeIndex].name} 오브젝트에 없습니다.");
            }
        }
    }
}
