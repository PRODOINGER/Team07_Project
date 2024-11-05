using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives;    // Life ������Ʈ �迭
    public Sprite mineHeart1;     // �浹 �� ������ ��������Ʈ
    public static UIManager Instance { get; private set; }
    private int lifeIndex;        // ���� ������ Life �ε���

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lifeIndex = lives.Length - 1; // Life 3���� �����ϵ��� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // �浹�� ���� Life ��������Ʈ ������Ʈ
    public void UpdateLifeImagesOnCollision()
    {
        if (lifeIndex >= 0 && lives[lifeIndex] != null)
        {
            Image lifeImage = lives[lifeIndex].GetComponent<Image>();
            if (lifeImage != null)
            {
                lifeImage.sprite = mineHeart1; // ��������Ʈ�� mine-heart1�� ����
                lifeIndex--; // ���� Life�� �̵� (3 -> 2 -> 1 ����)
            }
        }
        else
        {
            Debug.LogWarning("No more lives to update or object is null.");
        }
    }
}
