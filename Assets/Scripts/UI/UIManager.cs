using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives;    // Life ������Ʈ �迭
    public Sprite mineHeart1;     // �浹 �� ������ ��������Ʈ
    private int lifeIndex;        // ���� ������ Life �ε���


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
