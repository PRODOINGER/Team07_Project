using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;


public class ScoreManager : MonoBehaviour
{
    public Text curScoreTxt; // ���� ���� ǥ��
    public int curScore = 0;
    private bool canEarnPoints = true; // �÷��̾ ��ֹ�(Box)�� �浹�ߴ��� Ȯ���ϰ�, �浹�ϸ� ���� ���� ���ϵ��� ����
    

    protected virtual void UpdateScoreUI() // �ʿ�� Record ���� �߰�����
    {
        curScoreTxt.text = curScore.ToString(); // �������� text 
    }

    private void OnCollisionStay(Collision collision)
    {
        // �ӽ�)-�ڽ� �±װ� ���� ������Ʈ�� �浹�ϸ� fasle�� ����.
        canEarnPoints = !collision.gameObject.CompareTag("Box"); 
    }

    protected void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)) && canEarnPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.collider.CompareTag("NoBlink"))
                {
                    AddScore(1);
                }
            }
        }
    }

    public void AddScore(int score) // Record���� ���� ����
    {
        curScore += score; //���������� +1
        UpdateScoreUI(); // �׸��� ���ھ�UI�� ������Ʈ
    }
}


