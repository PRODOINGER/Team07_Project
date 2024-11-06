using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ����(���) ������Ʈ �迭 - ���� ������Ʈ �迭�μ� �÷��̾��� ����� ��Ÿ���ϴ�.
    public GameObject[] lives;

    // �浹 �� ������ ��������Ʈ �̹��� - �浹�� �߻����� �� ���� ���� ��������Ʈ�� �� ��������Ʈ�� �����մϴ�.
    public Sprite mineHeart1;

    // ���� ������ ����(Life) �ε��� - �迭�� ���� ��ġ�� �����ϸ�, �����ϴ� ������� ���˴ϴ�.
    private int lifeIndex;

    // �ʱ�ȭ �޼����, ��ũ��Ʈ�� ������ �� �� �� ȣ��˴ϴ�.
    private void Start()
    {
        // lifeIndex�� lives �迭�� ������ �ε����� �����Ͽ� ������ ���������� ���ʷ� ���ҽ�Ű���� �ʱ�ȭ�մϴ�.
        lifeIndex = lives.Length - 1;
    }

    // �浹 �� ���� ��������Ʈ�� ������Ʈ�ϴ� �޼���
    public void UpdateLifeImagesOnCollision()
    {
        // lifeIndex�� 0 �̻��̰�, lives �迭�� �ش� �ε����� null�� �ƴ� ��쿡�� ����˴ϴ�.
        if (lifeIndex >= 0 && lives[lifeIndex] != null)
        {
            // ���� lifeIndex ��ġ�� GameObject���� Image ������Ʈ�� �����ɴϴ�.
            Image lifeImage = lives[lifeIndex].GetComponent<Image>();

            // Image ������Ʈ�� ������ ��� (null�� �ƴ� ���) ���� ��������Ʈ�� �����մϴ�.
            if (lifeImage != null)
            {
                // ���� ���� ������Ʈ�� ��������Ʈ�� mineHeart1���� �����Ͽ� �浹�� �ð������� ǥ���մϴ�.
                lifeImage.sprite = mineHeart1;

                // lifeIndex�� �����Ͽ� ���� ���� ������Ʈ�� �̵� (������ ����: 3 -> 2 -> 1 ����)
                lifeIndex--;
            }
        }
        else
        {
            // lifeIndex�� ��ȿ���� �ʰų�, �ش� ������Ʈ�� null�� ��� ��� �޽����� ����մϴ�.
            Debug.LogWarning("No more lives to update or object is null.");
        }
    }
}
