using Supercyan.FreeSample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;               // ���� ĳ����
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // ĳ���Ϳ��� �Ÿ� ������
    [SerializeField] private float fixedYPosition = 5f;      // ������ Y ��ġ

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �� �̺�Ʈ ����
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ���� ����
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BCScene") // BCScene�� �ε�� ��츸 ����
        {
            if (CharacterControl.Instance != null)
            {
                target = CharacterControl.Instance.transform; // target�� CharacterControl�� �ν��Ͻ��� �Ҵ�
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Ÿ���� X, Z ��ġ�� �������� �����ϵ�, Y ��ġ�� ������ ��ġ ���
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, fixedYPosition, target.position.z + offset.z);

        // ī�޶� ��� Ÿ�� ��ġ�� �̵�
        transform.position = targetPosition;

        // ī�޶� ĳ���͸� �ٶ󺸵��� ����
        transform.LookAt(new Vector3(target.position.x, fixedYPosition, target.position.z)); // Y ��ġ ����
    }
}
