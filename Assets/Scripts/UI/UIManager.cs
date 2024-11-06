using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lives;    // Life 오브젝트 배열
    public Sprite mineHeart1;     // 충돌 시 변경할 스프라이트
    private int lifeIndex;        // 현재 변경할 Life 인덱스


    // 충돌에 따라 Life 스프라이트 업데이트
    public void UpdateLifeImagesOnCollision()
    {
        if (lifeIndex >= 0 && lives[lifeIndex] != null)
        {
            Image lifeImage = lives[lifeIndex].GetComponent<Image>();
            if (lifeImage != null)
            {
                lifeImage.sprite = mineHeart1; // 스프라이트를 mine-heart1로 변경
                lifeIndex--; // 다음 Life로 이동 (3 -> 2 -> 1 순서)
            }
        }
        else
        {
            Debug.LogWarning("No more lives to update or object is null.");
        }
    }
}
