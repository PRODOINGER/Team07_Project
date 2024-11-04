using UnityEngine;

namespace Supercyan.FreeSample
{
    public class AccessoryWearLogic : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer m_characterRenderer; // 캐릭터의 SkinnedMeshRenderer
        [SerializeField] private AccessoryLogic[] m_accessoriesToAttach = null; // 착용할 장신구 배열

        private Transform[] m_characterBones; // 캐릭터의 본 배열
        private bool m_initialized = false; // 초기화 여부 확인

        private void Initialize(GameObject character)
        {
            // 캐릭터의 SkinnedMeshRenderer가 설정되지 않았다면, 자식에서 찾아 설정
            if (m_characterRenderer == null)
            {
                m_characterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

                // SkinnedMeshRenderer가 없을 경우 경고 메시지 출력 후 초기화 중단
                if (m_characterRenderer == null)
                {
                    Debug.LogWarning("Missing character components. AccessoryWearLogic will not function.");
                    return;
                }
            }

            // 캐릭터의 rootBone이 없을 경우 경고 메시지 출력 후 초기화 중단
            if (m_characterRenderer.rootBone == null)
            {
                Debug.LogWarning("Missing character root bone. AccessoryWearLogic will not function.");
                return;
            }

            // 캐릭터 본 배열 설정
            m_characterBones = m_characterRenderer.bones;
            m_initialized = true;
        }

        private void Awake()
        {
            Initialize(gameObject);

            // 착용할 장신구가 설정된 경우에만 Attach 호출
            if (m_accessoriesToAttach != null)
            {
                foreach (AccessoryLogic accessory in m_accessoriesToAttach)
                {
                    if (accessory != null) Attach(accessory);
                }
            }
        }

        public void Attach(AccessoryLogic accessory)
        {
            // 초기화가 완료되지 않았을 경우 초기화 재시도
            if (!m_initialized)
            {
                Initialize(gameObject);

                // 초기화 실패 시 Attach 중단
                if (!m_initialized)
                {
                    Debug.LogWarning("AccessoryWearLogic not initialized correctly, can't attach accessories.");
                    return;
                }
            }

            // 장신구가 없거나 렌더러가 없을 경우 Attach 중단
            if (accessory == null || accessory.Renderer == null || accessory.Renderer.rootBone == null)
            {
                Debug.LogWarning("Invalid accessory or missing renderer/root bone. Attachment aborted.");
                return;
            }

            // 캐릭터 본 배열과 장신구 본 배열을 매칭
            Transform[] newBones = GetBones(accessory.Renderer.bones, m_characterBones);
            if (newBones == null)
            {
                Debug.LogWarning("Accessory with incompatible rig cannot be attached.");
                return;
            }

            // 장신구의 본과 루트 본 설정
            accessory.Renderer.bones = newBones;
            accessory.Renderer.rootBone = m_characterRenderer.rootBone;
        }

        private Transform[] GetBones(Transform[] accessoryBones, Transform[] characterBones)
        {
            Transform[] newBones = new Transform[accessoryBones.Length];

            for (int i = 0; i < accessoryBones.Length; i++)
            {
                // 캐릭터의 본 구조에서 일치하는 본 찾기
                Transform bone = FindBone(m_characterRenderer.rootBone, accessoryBones[i].name);
                if (bone == null) { return null; }
                newBones[i] = bone;
            }

            return newBones;
        }

        private Transform FindBone(Transform rootBone, string name)
        {
            // 본 이름이 일치하면 해당 본 반환
            if (rootBone.name == name) { return rootBone; }

            // 재귀적으로 자식 본에서 이름이 일치하는 본 찾기
            for (int i = 0; i < rootBone.childCount; i++)
            {
                Transform found = FindBone(rootBone.GetChild(i), name);
                if (found != null) { return found; }
            }
            return null; // 일치하는 본을 찾지 못하면 null 반환
        }
    }
}
