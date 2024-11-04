using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("BCScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void CharacherCustom()
    {
        SceneManager.LoadScene("CharacterCustom");
    }

    public void QuitBttn()
    {
        Application.Quit();
    }

}
