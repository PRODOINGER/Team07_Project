using UnityEngine;

namespace Supercyan.FreeSample
{
    public class AccessoryLogic : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer m_renderer = null; // 장신구의 SkinnedMeshRenderer를 참조하여 본(뼈대) 매핑과 렌더링을 담당합니다.
        public SkinnedMeshRenderer Renderer { get { return m_renderer; } } // 외부에서 장신구의 SkinnedMeshRenderer에 접근할 수 있도록 공개 속성으로 제공합니다.

        [SerializeField] private GameObject m_rig = null; // 장신구의 뼈대(Rig) 오브젝트를 참조합니다. 주로 임시로 사용되는 본 구조입니다.

        private void Awake() { Destroy(m_rig); } // Awake()에서 장신구의 뼈대(Rig) 오브젝트를 삭제하여 불필요한 리소스를 제거합니다.
    }
}
