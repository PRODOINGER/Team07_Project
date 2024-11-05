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
        private GameObject currentAccessoryInstanceHat;                // ���� ����� hat ��ű� �ν��Ͻ�
        private GameObject currentAccessoryInstanceBackpack;           // ���� ����� backpack ��ű� �ν��Ͻ�

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

            bool isHat = selectedAccessoryPrefab.name.Contains("hat");
            bool isBackpack = selectedAccessoryPrefab.name.Contains("backpack");

            GameObject currentAccessoryInstance = isHat ? currentAccessoryInstanceHat : currentAccessoryInstanceBackpack;
            AccessoryLogic accessoryLogic = currentAccessoryInstance?.GetComponent<AccessoryLogic>();

            // ���� ���� Ȯ�� �� ����
            if (currentAccessoryInstance != null && accessoryWearLogic.IsEquipped(currentAccessoryInstance))
            {
                accessoryWearLogic.Detach(accessoryLogic); // ��ű� ����
                Destroy(currentAccessoryInstance); // �ν��Ͻ� ����

                // �ش� ��ű� ���� �ν��Ͻ� ���� ����
                if (isHat)
                    currentAccessoryInstanceHat = null;
                else if (isBackpack)
                    currentAccessoryInstanceBackpack = null;
            }
            else
            {
                // �� ��ű� �ν��Ͻ��� �����ϰ� ����
                currentAccessoryInstance = Instantiate(selectedAccessoryPrefab, character.transform);
                accessoryLogic = currentAccessoryInstance.GetComponent<AccessoryLogic>();

                if (CanEquipAccessory(accessoryLogic))
                {
                    accessoryWearLogic.Attach(accessoryLogic); // ��ű� ����

                    // ������ ��ű��� �ش� ������ ����
                    if (isHat)
                        currentAccessoryInstanceHat = currentAccessoryInstance;
                    else if (isBackpack)
                        currentAccessoryInstanceBackpack = currentAccessoryInstance;
                }
            }

            UpdateAccessoryButtonState();
        }

        // ��ű� ���� ���� ���� Ȯ�� (hat�� backpack�� ���������� ���� �����ϰ� ��)
        private bool CanEquipAccessory(AccessoryLogic accessory)
        {
            bool isHat = accessory.name.Contains("hat");
            bool isBackpack = accessory.name.Contains("backpack");

            // ���� ������ ��ű� ������ ���� ���� ���� Ȯ��
            if (isHat) return currentAccessoryInstanceHat == null;
            if (isBackpack) return currentAccessoryInstanceBackpack == null;

            return false;
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

            bool isEquipped = false;
            if (selectedAccessoryPrefab.name.Contains("hat"))
            {
                isEquipped = currentAccessoryInstanceHat != null && accessoryWearLogic.IsEquipped(currentAccessoryInstanceHat);
            }
            else if (selectedAccessoryPrefab.name.Contains("backpack"))
            {
                isEquipped = currentAccessoryInstanceBackpack != null && accessoryWearLogic.IsEquipped(currentAccessoryInstanceBackpack);
            }

            accessoryEquipOrUnequipButton.GetComponentInChildren<Text>().text = isEquipped ? "��ű� ����" : "��ű� ����";
        }
    }
}
