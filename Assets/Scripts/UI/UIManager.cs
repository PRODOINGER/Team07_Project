using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 생명(목숨) 오브젝트 배열 - 게임 오브젝트 배열로서 플레이어의 목숨을 나타냅니다.
    public GameObject[] lives;

    // 충돌 시 변경할 스프라이트 이미지 - 충돌이 발생했을 때 기존 생명 스프라이트를 이 스프라이트로 변경합니다.
    public Sprite mineHeart1;

    // 현재 변경할 생명(Life) 인덱스 - 배열의 현재 위치를 저장하며, 감소하는 방식으로 사용됩니다.
    private int lifeIndex;

    // 초기화 메서드로, 스크립트가 시작할 때 한 번 호출됩니다.
    private void Start()
    {
        // lifeIndex를 lives 배열의 마지막 인덱스로 설정하여 생명을 마지막부터 차례로 감소시키도록 초기화합니다.
        lifeIndex = lives.Length - 1;
    }

    // 충돌 시 생명 스프라이트를 업데이트하는 메서드
    public void UpdateLifeImagesOnCollision()
    {
        // lifeIndex가 0 이상이고, lives 배열의 해당 인덱스가 null이 아닐 경우에만 실행됩니다.
        if (lifeIndex >= 0 && lives[lifeIndex] != null)
        {
            // 현재 lifeIndex 위치의 GameObject에서 Image 컴포넌트를 가져옵니다.
            Image lifeImage = lives[lifeIndex].GetComponent<Image>();

            // Image 컴포넌트가 존재할 경우 (null이 아닐 경우) 생명 스프라이트를 변경합니다.
            if (lifeImage != null)
            {
                // 현재 생명 오브젝트의 스프라이트를 mineHeart1으로 변경하여 충돌을 시각적으로 표시합니다.
                lifeImage.sprite = mineHeart1;

                // lifeIndex를 감소하여 다음 생명 오브젝트로 이동 (순서는 역순: 3 -> 2 -> 1 순서)
                lifeIndex--;
            }
        }
        else
        {
            // lifeIndex가 유효하지 않거나, 해당 오브젝트가 null일 경우 경고 메시지를 출력합니다.
            Debug.LogWarning("No more lives to update or object is null.");
        }
    }
}
