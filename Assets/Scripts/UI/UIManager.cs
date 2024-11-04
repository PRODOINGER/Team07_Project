using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives;    //  이미지 배열

    // 충돌에 따라 이미지 업데이트
    public void UpdateLifeImages(int collisionCount)
    {
        if (collisionCount <= lives.Length)
        {
            lives[collisionCount - 1].SetActive(false); // 이미지 비활성화
        }
    }
}
