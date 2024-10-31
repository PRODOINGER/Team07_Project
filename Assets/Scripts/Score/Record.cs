using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Record : MonoBehaviour
{
    private int highScore = 0; // �� ó�� �����Ҷ� �� = 0
    public int curScore = 0; // ���������� 0���� ����. 

    // ���������� �ְ����� ��������
    public (int highScore, int curScore) GetScores()
    {
        return (highScore, curScore);
    }

    // ���������� ���ؼ� �ְ��������� ������ �ְ������� �����ϱ�
    public void CheckAndSetHighScore(int currentScore)
    {
        curScore = currentScore; // �������� ������Ʈ

        if (curScore > highScore) // ���� ���������� �ְ��������� ���ٸ�
        {
            highScore = curScore; // �ְ������� ���������� 
            SaveHighScore(); // �׸��� ����
        }
    }

    // ����� �ְ������� ������ȭ �Ͽ� �����ϱ�
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); // �ְ����� ���� �ְ������� ����
        PlayerPrefs.Save(); // �׸��� ����
    }

    //�ʿ��Ҷ� �ε�
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
