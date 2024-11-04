using System.Collections.Generic;
using UnityEngine;

namespace Supercyan.FreeSample
{
    public class AccessoryWearLogic : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer m_characterRenderer;
        [SerializeField] private AccessoryLogic[] m_accessoriesToAttach;

        private List<AccessoryLogic> equippedAccessories = new List<AccessoryLogic>(); // 장착된 액세서리 리스트

        private Transform[] m_characterBones;
        private bool m_initialized = false;

        private void Initialize(GameObject character)
        {
            if (m_characterRenderer == null)
            {
                m_characterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

                if (m_characterRenderer == null)
                {
                    Debug.LogWarning("Missing character components.");
                    return;
                }
            }
            if (m_characterRenderer.rootBone == null)
            {
                Debug.LogWarning("Missing character root bone.");
                return;
            }
            m_characterBones = m_characterRenderer.bones;
            m_initialized = true;
        }

        private void Awake()
        {
            Initialize(gameObject);

            if (m_accessoriesToAttach != null)
            {
                foreach (AccessoryLogic accessory in m_accessoriesToAttach)
                {
                    Attach(accessory);
                }
            }
        }

        public void Attach(AccessoryLogic accessory)
        {
            if (!m_initialized)
            {
                Initialize(gameObject);
                if (!m_initialized) return;
            }
            if (accessory == null || accessory.Renderer == null || accessory.Renderer.rootBone == null) return;

            Transform[] newBones = GetBones(accessory.Renderer.bones, m_characterBones);
            if (newBones == null) return;

            accessory.Renderer.bones = newBones;
            accessory.Renderer.rootBone = m_characterRenderer.rootBone;
            equippedAccessories.Add(accessory);
        }

        public void Detach(AccessoryLogic accessory)
        {
            if (equippedAccessories.Contains(accessory))
            {
                equippedAccessories.Remove(accessory);
                accessory.Renderer.enabled = false; // 장신구 비활성화
            }
        }

        public List<AccessoryLogic> GetEquippedAccessories()
        {
            return equippedAccessories;
        }

        public bool IsEquipped(GameObject accessory)
        {
            return equippedAccessories.Exists(e => e.gameObject == accessory);
        }

        private Transform[] GetBones(Transform[] accessoryBones, Transform[] characterBones)
        {
            Transform[] newBones = new Transform[accessoryBones.Length];

            for (int i = 0; i < accessoryBones.Length; i++)
            {
                Transform bone = FindBone(m_characterRenderer.rootBone, accessoryBones[i].name);
                if (bone == null) { return null; }
                newBones[i] = bone;
            }
            return newBones;
        }

        private Transform FindBone(Transform rootBone, string name)
        {
            if (rootBone.name == name) return rootBone;

            for (int i = 0; i < rootBone.childCount; i++)
            {
                Transform found = FindBone(rootBone.GetChild(i), name);
                if (found != null) return found;
            }
            return null;
        }
    }
}
