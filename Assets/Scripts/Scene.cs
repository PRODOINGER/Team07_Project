using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void GameStart()
    {
        // 씬 전환 전에 미리보기 장신구 삭제
        CharacterCustomizationRoomManager.Instance?.DeletePreviewAccessories();
        SceneManager.LoadScene("BCScene");
    }

    public void StartScene()
    {
        // 씬 전환 전에 미리보기 장신구 삭제
        CharacterCustomizationRoomManager.Instance?.DeletePreviewAccessories();
        SceneManager.LoadScene("Start");
    }

    public void CharacherCustom()
    {
        // 씬 전환 전에 미리보기 장신구 삭제
        CharacterCustomizationRoomManager.Instance?.DeletePreviewAccessories();
        SceneManager.LoadScene("CharacterCustom");
    }

    public void QuitBttn()
    {
        Application.Quit();
    }
}
