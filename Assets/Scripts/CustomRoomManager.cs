using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supercyan.FreeSample
{
    public class CharacterCustomizationRoomManager : MonoBehaviour
    {
        [SerializeField] private Button accessoryEquipOrUnequipButton; // ��ű� ����/���� ��ư
        [SerializeField] private Button accessoryNextButton;           // ���� ��ű� ���� ��ư

        private CharacterControl character;                            // Player �±׸� ���� ĳ���� ����
        private AccessoryWearLogic accessoryWearLogic;                 // ĳ������ ��ű� ���� ����

        private List<GameObject> accessories;                          // ��ű� ���
        private int currentAccessoryIndex = 0;                         // ���� ���õ� ��ű� �ε���
        private GameObject selectedAccessoryPrefab;                    // ���� ���õ� ��ű� ������
        private GameObject currentAccessoryInstance;                   // ���� ������ ��ű� �ν��Ͻ�

        private void Start()
        {
            character = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterControl>();
            accessoryWearLogic = character?.GetComponent<AccessoryWearLogic>();

            if (accessoryWearLogic == null)
            {
                Debug.LogError("AccessoryWearLogic�� ĳ���Ϳ� �Ҵ���� �ʾҽ��ϴ�.");
                return;
            }

            accessories = new List<GameObject>
            {
                Resources.Load<GameObject>("backpack"),
                Resources.Load<GameObject>("hat_blue"),
                Resources.Load<GameObject>("hat_orange"),
                Resources.Load<GameObject>("hat_pink"),
                Resources.Load<GameObject>("hat_white")
            };

            accessoryEquipOrUnequipButton.onClick.AddListener(EquipOrUnequipAccessory);
            accessoryNextButton.onClick.AddListener(SelectNextAccessory);

            selectedAccessoryPrefab = accessories.Count > 0 ? accessories[0] : null;
            UpdateAccessoryButtonState();
        }

        private void EquipOrUnequipAccessory()
        {
            if (selectedAccessoryPrefab == null || accessoryWearLogic == null) return;

            AccessoryLogic accessoryLogic = currentAccessoryInstance?.GetComponent<AccessoryLogic>();

            if (currentAccessoryInstance != null && accessoryWearLogic.IsEquipped(currentAccessoryInstance))
            {
                accessoryWearLogic.Detach(accessoryLogic); // ��ű� ����
                Destroy(currentAccessoryInstance); // �ν��Ͻ� ����
                currentAccessoryInstance = null;
            }
            else
            {
                currentAccessoryInstance = Instantiate(selectedAccessoryPrefab, character.transform);
                accessoryLogic = currentAccessoryInstance.GetComponent<AccessoryLogic>();

                if (CanEquipAccessory(accessoryLogic))
                {
                    accessoryWearLogic.Attach(accessoryLogic); // ��ű� ����
                }
            }

            UpdateAccessoryButtonState();
        }

        private bool CanEquipAccessory(AccessoryLogic accessory)
        {
            bool hasHat = false;

            foreach (AccessoryLogic equipped in accessoryWearLogic.GetEquippedAccessories())
            {
                if (equipped.name.Contains("hat")) hasHat = true;
                if (equipped.name.Contains("hat") && accessory.name.Contains("hat")) return false;
            }

            return !hasHat || !accessory.name.Contains("hat");
        }

        private void SelectNextAccessory()
        {
            currentAccessoryIndex = (currentAccessoryIndex + 1) % accessories.Count;
            selectedAccessoryPrefab = accessories[currentAccessoryIndex];
            UpdateAccessoryButtonState();
        }

        private void UpdateAccessoryButtonState()
        {
            if (accessoryWearLogic == null || selectedAccessoryPrefab == null) return;

            bool isEquipped = currentAccessoryInstance != null && accessoryWearLogic.IsEquipped(currentAccessoryInstance);

            accessoryEquipOrUnequipButton.GetComponentInChildren<Text>().text = isEquipped ? "��ű� ����" : "��ű� ����";
        }
    }
}
