using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supercyan.FreeSample
{
    public class CharacterCustomizationRoomManager : MonoBehaviour
    {
        [SerializeField] private Button accessoryEquipOrUnequipButton; // 장신구 장착/해제 버튼
        [SerializeField] private Button accessoryNextButton;           // 다음 장신구 선택 버튼

        private CharacterControl character;                            // Player 태그를 가진 캐릭터 참조
        private AccessoryWearLogic accessoryWearLogic;                 // 캐릭터의 장신구 착용 관리

        private List<GameObject> accessories;                          // 장신구 목록
        private int currentAccessoryIndex = 0;                         // 현재 선택된 장신구 인덱스
        private GameObject selectedAccessoryPrefab;                    // 현재 선택된 장신구 프리팹
        private GameObject currentAccessoryInstance;                   // 현재 장착된 장신구 인스턴스

        private void Start()
        {
            character = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterControl>();
            accessoryWearLogic = character?.GetComponent<AccessoryWearLogic>();

            if (accessoryWearLogic == null)
            {
                Debug.LogError("AccessoryWearLogic이 캐릭터에 할당되지 않았습니다.");
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
                accessoryWearLogic.Detach(accessoryLogic); // 장신구 해제
                Destroy(currentAccessoryInstance); // 인스턴스 삭제
                currentAccessoryInstance = null;
            }
            else
            {
                currentAccessoryInstance = Instantiate(selectedAccessoryPrefab, character.transform);
                accessoryLogic = currentAccessoryInstance.GetComponent<AccessoryLogic>();

                if (CanEquipAccessory(accessoryLogic))
                {
                    accessoryWearLogic.Attach(accessoryLogic); // 장신구 착용
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

            accessoryEquipOrUnequipButton.GetComponentInChildren<Text>().text = isEquipped ? "장신구 해제" : "장신구 착용";
        }
    }
}
