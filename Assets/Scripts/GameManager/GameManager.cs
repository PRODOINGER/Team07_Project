using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text CurScore;
    public Text CurScoreNum;

    public Button startButton;
    public ScoreManager scoreManager;

    void Start()
    {
        // ���� ���� �ؽ�Ʈ �����
        CurScore.gameObject.SetActive(false);
        CurScoreNum.gameObject.SetActive(false);

        // ��ư�� StartGame �޼��� ����
        startButton.onClick.AddListener(StartGame);

        // ScoreManager�� �����ϸ� �ʱ� ���� UI ������Ʈ
        if (scoreManager != null)
        {
            scoreManager.UpdateScoreUI(); 
        }
    }

    void StartGame()
    {
        CurScore.gameObject.SetActive(true);
       CurScoreNum.gameObject.SetActive(true);

        // ���� �ʱ�ȭ �� UI ������Ʈ
        if (scoreManager != null)
        {
            scoreManager.AddScore(0); // �ʱ�ȭ �뵵�� 0�� �߰�
        }
    }

}
