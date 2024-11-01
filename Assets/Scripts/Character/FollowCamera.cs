using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;             // 따라갈 캐릭터
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // 캐릭터와의 거리 오프셋
    [SerializeField] private float fixedYPosition = 5f;    // 고정된 Y 위치

    private void LateUpdate()
    {
        if (target == null)
            return;

        // 타겟의 X, Z 위치에 오프셋을 적용하되, Y 위치는 고정된 위치 사용
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, fixedYPosition, target.position.z + offset.z);

        // 카메라를 즉시 타겟 위치로 이동
        transform.position = targetPosition;

        // 카메라가 캐릭터를 바라보도록 설정
        transform.LookAt(new Vector3(target.position.x, fixedYPosition, target.position.z)); // Y 위치 고정
    }
}
