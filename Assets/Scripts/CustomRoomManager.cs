using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supercyan.FreeSample
{
    public class CharacterCustomizationRoomManager : MonoBehaviour
    {
        public static CharacterCustomizationRoomManager Instance { get; private set; } // 싱글톤 인스턴스
        [SerializeField] private CharacterControl characterControl; // 캐릭터 제어 스크립트
        [SerializeField] private AccessoryWearLogic accessoryWearLogic; // 캐릭터 장신구 착용 로직
        [SerializeField] private Button poseToggleButton; // 캐릭터 포즈 전환 버튼
        [SerializeField] private Button accessoryEquipButton; // 장신구 장착 버튼
        [SerializeField] private Button accessoryNextButton; // 다음 장신구 선택 버튼
        [SerializeField] private Button accessoryUnequipButton; // 장신구 해제 버튼

        private List<GameObject> accessories; // 장신구 목록
        private int currentAccessoryIndex = 0; // 현재 선택된 장신구 인덱스
        private bool characterIsPosed = false; // 캐릭터가 포즈 중인지 여부
        private GameObject selectedAccessory; // 현재 선택된 장신구 오브젝트

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 씬 전환해도 유지
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            // Player 태그가 붙은 오브젝트에서 CharacterControl 스크립트를 찾아서 Rigidbody 제어
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                characterControl = player.GetComponent<CharacterControl>();
                if (characterControl != null)
                {
                    characterControl.enabled = true; // 캐릭터가 씬에서 멈추지 않도록 활성화
                    characterControl.FreezeCharacter(true); // 움직임 정지 로직 추가
                }
            }

            // 장신구 리스트 초기화
            accessories = new List<GameObject>
            {
                Resources.Load<GameObject>("backpack"),
                Resources.Load<GameObject>("hat_blue"),
                Resources.Load<GameObject>("hat_orange"),
                Resources.Load<GameObject>("hat_pink"),
                Resources.Load<GameObject>("hat_white")
            };

            // 버튼 클릭 이벤트 설정
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
                characterControl.FreezeCharacter(!characterIsPosed); // 캐릭터 포즈 전환
            }
        }

        private void EquipAccessory()
        {
            if (selectedAccessory == null) return;

            AccessoryLogic accessoryLogic = selectedAccessory.GetComponent<AccessoryLogic>();
            if (accessoryLogic == null) return;

            if (CanEquipAccessory(accessoryLogic))
            {
                accessoryWearLogic.Attach(accessoryLogic); // 장신구 착용
                UpdateAccessoryButtons(); // 버튼 상태 갱신
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

            accessoryWearLogic.Detach(accessoryLogic); // 장신구 해제
            UpdateAccessoryButtons(); // 버튼 상태 갱신
        }

        private void UpdateAccessoryButtons()
        {
            bool isEquipped = accessoryWearLogic.IsEquipped(selectedAccessory);

            accessoryEquipButton.interactable = !isEquipped;
            accessoryUnequipButton.interactable = isEquipped;
        }
    }
}
