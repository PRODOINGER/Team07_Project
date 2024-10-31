using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;


public class ScoreManager : MonoBehaviour
{
    public Text curScoreTxt; // ���� ���� ǥ��
    protected int curScore = 0;
    protected bool canEarnPoints = true; // �÷��̾ ��ֹ�(Box)�� �浹�ߴ��� Ȯ���ϰ�, �浹�ϸ� ���� ���� ���ϵ��� ����

    protected virtual void UpdateScoreUI() // �ʿ�� Record ���� �߰�����
    {
        curScoreTxt.text = curScore.ToString(); // �������� text 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �ӽ�)-�ڽ� �±װ� ���� ������Ʈ�� �浹�ϸ� fasle�� ����.
        canEarnPoints = !collision.gameObject.CompareTag("Box");
    }

    protected void Update()
    {
        // Spacebar�� ���� ��� canEarnPoints�� Ȯ��
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canEarnPoints)
        {
            AddScore(1); // true �۵��� +1�� �ż��� �۵�
        }
    }

    protected void AddScore(int score) // Record���� ���� ����
    {
        curScore += score; //���������� +1
        UpdateScoreUI(); // �׸��� ���ھ�UI�� ������Ʈ
    }
}

