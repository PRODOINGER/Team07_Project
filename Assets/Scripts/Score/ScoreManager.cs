using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreManager : MonoBehaviour
{
    public Text curScoreTxt; // ���� ���� ǥ��
    public int curScore = 0;

    public virtual void UpdateScoreUI() // �ʿ�� Record ���� �߰�����
    {
        curScoreTxt.text = curScore.ToString(); // �������� text 
    }

    protected void Update()
    {
        
    }

    public void AddScore(int score) // Record���� ���� ����
    {
        curScore += score; //���������� +1
        UpdateScoreUI(); // �׸��� ���ھ�UI�� ������Ʈ
    }
}

