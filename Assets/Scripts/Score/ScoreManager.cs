using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI CurScoreText;
    public TextMeshProUGUI CurScoreNum; 
    public int CurScoreNumValue = 0;
    private int highScore = 0;

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        // �̱��� ���� ����: �ν��Ͻ��� ���ٸ� ���� �ν��Ͻ��� ����ϰ� �ߺ��� ��� �ı�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �Ŀ��� ������Ʈ ���� // �θ�ü�� ������ �ȵ�
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ��� �ߺ� ����
            return;
        }
    }
    private void Start()
    {
        StartScoreUI();
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
        CurScoreNumValue = 0; // �ʱ�ȭ �뵵�� 0�� �߰�
        UpdateScoreUI(); // �ʱ� ���� ǥ��
    }

    public virtual void UpdateScoreUI()
    {
        if (CurScoreNum != null)
        {
            CurScoreNum.text = CurScoreNumValue.ToString(); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Score"))
        {
            AddScore(1);           
            Debug.Log("������ ������ϴ�!");
            Destroy(other.gameObject); 
        }
    }

    public void AddScore(int score)
    {
        CurScoreNumValue += score; // ���� ������ �߰�
        UpdateScoreUI(); // ���ھ� UI�� ������Ʈ
        CheckAndSetHighScore();
    }
   
    private void CheckAndSetHighScore()
    {
        if (CurScoreNumValue > highScore) // ���� ���� ������ �ְ� �������� ���ٸ�
        {
            highScore = CurScoreNumValue; // �ְ� ������ ���� ������ ������Ʈ
            SaveHighScore(); // �׸��� ����
        }
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); // �ְ� ������ ����
        PlayerPrefs.Save(); // PlayerPrefs ����
    }
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // ����� �ְ� ���� �ε� (�⺻�� 0)
    }


    
}

