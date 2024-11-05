using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives; // Life ������Ʈ �迭
    public Sprite mineHeart1; // �浹 �� ������ ��������Ʈ
    public static UIManager Instance { get; private set; }
    private int lifeIndex; // ���� ������ Life �ε���

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
            // BC Scene���� "Life" �±׷� ��� ������Ʈ ã��
            lives = GameObject.FindGameObjectsWithTag("Life");
            lifeIndex = lives.Length - 1; // lifeIndex �ʱ�ȭ
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
                Debug.LogError($"Image component�� {lives[lifeIndex].name} ������Ʈ�� �����ϴ�.");
            }
        }
    }
}
