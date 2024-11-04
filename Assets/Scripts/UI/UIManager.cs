using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives;    //  �̹��� �迭

    // �浹�� ���� �̹��� ������Ʈ
    public void UpdateLifeImages(int collisionCount)
    {
        if (collisionCount <= lives.Length)
        {
            lives[collisionCount - 1].SetActive(false); // �̹��� ��Ȱ��ȭ
        }
    }
}
