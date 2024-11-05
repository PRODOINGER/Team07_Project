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
        private GameObject currentAccessoryInstanceHat;                // 현재 착용된 hat 장신구 인스턴스
        private GameObject currentAccessoryInstanceBackpack;           // 현재 착용된 backpack 장신구 인스턴스

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

            bool isHat = selectedAccessoryPrefab.name.Contains("hat");
            bool isBackpack = selectedAccessoryPrefab.name.Contains("backpack");

            GameObject currentAccessoryInstance = isHat ? currentAccessoryInstanceHat : currentAccessoryInstanceBackpack;
            AccessoryLogic accessoryLogic = currentAccessoryInstance?.GetComponent<AccessoryLogic>();

            // 장착 상태 확인 후 해제
            if (currentAccessoryInstance != null && accessoryWearLogic.IsEquipped(currentAccessoryInstance))
            {
                accessoryWearLogic.Detach(accessoryLogic); // 장신구 해제
                Destroy(currentAccessoryInstance); // 인스턴스 삭제

                // 해당 장신구 유형 인스턴스 변수 비우기
                if (isHat)
                    currentAccessoryInstanceHat = null;
                else if (isBackpack)
                    currentAccessoryInstanceBackpack = null;
            }
            else
            {
                // 새 장신구 인스턴스를 생성하고 장착
                currentAccessoryInstance = Instantiate(selectedAccessoryPrefab, character.transform);
                accessoryLogic = currentAccessoryInstance.GetComponent<AccessoryLogic>();

                if (CanEquipAccessory(accessoryLogic))
                {
                    accessoryWearLogic.Attach(accessoryLogic); // 장신구 착용

                    // 착용한 장신구를 해당 변수에 저장
                    if (isHat)
                        currentAccessoryInstanceHat = currentAccessoryInstance;
                    else if (isBackpack)
                        currentAccessoryInstanceBackpack = currentAccessoryInstance;
                }
            }

            UpdateAccessoryButtonState();
        }

        // 장신구 착용 가능 여부 확인 (hat과 backpack을 독립적으로 착용 가능하게 함)
        private bool CanEquipAccessory(AccessoryLogic accessory)
        {
            bool isHat = accessory.name.Contains("hat");
            bool isBackpack = accessory.name.Contains("backpack");

            // 현재 선택한 장신구 유형에 따라 착용 상태 확인
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

            accessoryEquipOrUnequipButton.GetComponentInChildren<Text>().text = isEquipped ? "장신구 해제" : "장신구 착용";
        }
    }
}
