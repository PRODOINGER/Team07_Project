using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void GameStart()
    {
        // �� ��ȯ ���� �̸����� ��ű� ����
        CharacterCustomizationRoomManager.Instance?.DeletePreviewAccessories();
        SceneManager.LoadScene("BCScene");
    }

    public void StartScene()
    {
        // �� ��ȯ ���� �̸����� ��ű� ����
        CharacterCustomizationRoomManager.Instance?.DeletePreviewAccessories();
        SceneManager.LoadScene("Start");
    }

    public void CharacherCustom()
    {
        // �� ��ȯ ���� �̸����� ��ű� ����
        CharacterCustomizationRoomManager.Instance?.DeletePreviewAccessories();
        SceneManager.LoadScene("CharacterCustom");
    }

    public void QuitBttn()
    {
        Application.Quit();
    }
}
