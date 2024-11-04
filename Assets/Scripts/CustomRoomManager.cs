using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supercyan.FreeSample
{
    public class CharacterCustomizationRoomManager : MonoBehaviour
    {
        public static CharacterCustomizationRoomManager Instance { get; private set; } // �̱��� �ν��Ͻ�
        [SerializeField] private CharacterControl characterControl; // ĳ���� ���� ��ũ��Ʈ
        [SerializeField] private AccessoryWearLogic accessoryWearLogic; // ĳ���� ��ű� ���� ����
        [SerializeField] private Button poseToggleButton; // ĳ���� ���� ��ȯ ��ư
        [SerializeField] private Button accessoryEquipButton; // ��ű� ���� ��ư
        [SerializeField] private Button accessoryNextButton; // ���� ��ű� ���� ��ư
        [SerializeField] private Button accessoryUnequipButton; // ��ű� ���� ��ư

        private List<GameObject> accessories; // ��ű� ���
        private int currentAccessoryIndex = 0; // ���� ���õ� ��ű� �ε���
        private bool characterIsPosed = false; // ĳ���Ͱ� ���� ������ ����
        private GameObject selectedAccessory; // ���� ���õ� ��ű� ������Ʈ

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // �� ��ȯ�ص� ����
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            // Player �±װ� ���� ������Ʈ���� CharacterControl ��ũ��Ʈ�� ã�Ƽ� Rigidbody ����
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                characterControl = player.GetComponent<CharacterControl>();
                if (characterControl != null)
                {
                    characterControl.enabled = true; // ĳ���Ͱ� ������ ������ �ʵ��� Ȱ��ȭ
                    characterControl.FreezeCharacter(true); // ������ ���� ���� �߰�
                }
            }

            // ��ű� ����Ʈ �ʱ�ȭ
            accessories = new List<GameObject>
            {
                Resources.Load<GameObject>("backpack"),
                Resources.Load<GameObject>("hat_blue"),
                Resources.Load<GameObject>("hat_orange"),
                Resources.Load<GameObject>("hat_pink"),
                Resources.Load<GameObject>("hat_white")
            };

            // ��ư Ŭ�� �̺�Ʈ ����
            accessoryEquipButton.onClick.AddListener(EquipAccessory);
            accessoryNextButton.onClick.AddListener(SelectNextAccessory);
            accessoryUnequipButton.onClick.AddListener(UnequipAccessory);
            poseToggleButton.onClick.AddListener(ToggleCharacterPose);

            UpdateAccessoryButtons();
        }

        private void ToggleCharacterPose()
        {
            if (characterControl != null)
            {
                characterIsPosed = !characterIsPosed;
                characterControl.FreezeCharacter(!characterIsPosed); // ĳ���� ���� ��ȯ
            }
        }

        private void EquipAccessory()
        {
            if (selectedAccessory == null) return;

            AccessoryLogic accessoryLogic = selectedAccessory.GetComponent<AccessoryLogic>();
            if (accessoryLogic == null) return;

            if (CanEquipAccessory(accessoryLogic))
            {
                accessoryWearLogic.Attach(accessoryLogic); // ��ű� ����
                UpdateAccessoryButtons(); // ��ư ���� ����
            }
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
            selectedAccessory = accessories[currentAccessoryIndex];

            UpdateAccessoryButtons();
        }

        private void UnequipAccessory()
        {
            if (selectedAccessory == null) return;

            AccessoryLogic accessoryLogic = selectedAccessory.GetComponent<AccessoryLogic>();
            if (accessoryLogic == null) return;

            accessoryWearLogic.Detach(accessoryLogic); // ��ű� ����
            UpdateAccessoryButtons(); // ��ư ���� ����
        }

        private void UpdateAccessoryButtons()
        {
            bool isEquipped = accessoryWearLogic.IsEquipped(selectedAccessory);

            accessoryEquipButton.interactable = !isEquipped;
            accessoryUnequipButton.interactable = isEquipped;
        }
    }
}
