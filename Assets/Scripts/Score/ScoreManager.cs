using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreNum;
    public TextMeshProUGUI CurScoreText;
    public TextMeshProUGUI CurScoreNum;
    public int CurScoreNumValue = 0;
    private int highScore = 0;

    private void Start()
    {
        StartScoreUI();
        LoadHighScore(); // ���� �ְ� ���� �ҷ�����

        // �ְ� ���� �ʱ� ǥ��
        if (HighScoreNum != null)
        {
            HighScoreNum.text = highScore.ToString();
        }

        InvokeRepeating("AddScoreEverySecond", 1f, 1f);
    }

    // ���� UI�� �ʱ�ȭ�ϰ� Ȱ��ȭ�ϴ� �޼���
    public void StartScoreUI()
    {
        if (CurScoreText != null)
        {
            CurScoreText.gameObject.SetActive(true);
        }
        if (CurScoreNum != null)
        {
            CurScoreNum.gameObject.SetActive(true);
        }

        // ���� �ʱ�ȭ �� UI ������Ʈ
        CurScoreNumValue = 0;
        UpdateScoreUI();
    }

    public virtual void UpdateScoreUI()
    {
        if (CurScoreNum != null)
        {
            CurScoreNum.text = CurScoreNumValue.ToString();
        }
    }

    // ���� �ð��� ���� 1���� �߰��ϴ� �޼���
    private void AddScoreEverySecond()
    {
        AddScore(1); // 1�ʸ��� ���� 1���� ����
    }

    public void AddScore(int score)
    {
        CurScoreNumValue += score; // ���� ������ �߰�
        UpdateScoreUI(); // ���ھ� UI�� ������Ʈ
        CheckAndSetHighScore();
        Debug.Log("����ȹ��");
    }

    private void CheckAndSetHighScore()
    {
        if (CurScoreNumValue > highScore)
        {
            highScore = CurScoreNumValue;
            SaveHighScore();

            // �ְ� ���� UI ���� ������Ʈ
            if (HighScoreNum != null)
            {
                HighScoreNum.text = highScore.ToString();
            }
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
