using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public GameObject[] lives;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // �̱��� ���� ����: �ν��Ͻ��� ���ٸ� ���� �ν��Ͻ��� ����ϰ� �ߺ��� ��� �ı�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �Ŀ��� ������Ʈ ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ��� �ߺ� ����
            return;
        }
    }
    // �浹�� ���� �̹��� ������Ʈ
    public void UpdateLifeImages(int collisionCount)
    {
        if (collisionCount > 0 && collisionCount <= lives.Length)
        {
            lives[collisionCount - 1].SetActive(false); // �ش� �̹��� ��Ȱ��ȭ
        }
    }
}
