using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Supercyan.FreeSample
{
    public class CharacterCustomizationRoomManager : MonoBehaviour
    {
        // 싱글톤 인스턴스 선언
        public static CharacterCustomizationRoomManager Instance { get; private set; }

        [SerializeField] private Button accessoryConfirmButton;        // 장신구 확정 착용 버튼
        [SerializeField] private Button accessoryRemoveButton;         // 장신구 해제 버튼
        [SerializeField] private Button accessoryNextButton;           // 다음 장신구 선택 버튼

        private CharacterControl character;                            // Player 태그를 가진 캐릭터 참조
        private AccessoryWearLogic accessoryWearLogic;                 // 캐릭터의 장신구 착용 관리

        private List<GameObject> accessories;                          // 장신구 목록
        private int currentAccessoryIndex = 0;                         // 현재 선택된 장신구 인덱스
        private GameObject selectedAccessoryPrefab;                    // 현재 선택된 장신구 프리팹

        private GameObject previewAccessoryInstanceHat;                // 미리보기 hat 장신구 인스턴스
        private GameObject previewAccessoryInstanceBackpack;           // 미리보기 backpack 장신구 인스턴스
        private GameObject confirmedAccessoryInstanceHat;              // 확정 착용된 hat 장신구 인스턴스
        private GameObject confirmedAccessoryInstanceBackpack;         // 확정 착용된 backpack 장신구 인스턴스

        private void Awake()
        {
            // 싱글톤 인스턴스 설정
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // 인스턴스 중복 방지
                return;
            }
        }

        private void Start()
        {
            character = CharacterControl.Instance;
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

            accessoryConfirmButton.onClick.AddListener(ConfirmAccessory);
            accessoryRemoveButton.onClick.AddListener(RemoveConfirmedAccessory);
            accessoryNextButton.onClick.AddListener(SelectNextAccessory);

            selectedAccessoryPrefab = accessories.Count > 0 ? accessories[0] : null;
            ShowAccessoryPreview();
        }

        // 미리보기 장신구를 삭제하고, 확정 장착된 장신구의 Renderer를 활성화하는 메서드
        public void DeletePreviewAccessories()
        {
            // 미리보기 장신구 삭제
            if (previewAccessoryInstanceHat != null) Destroy(previewAccessoryInstanceHat);
            if (previewAccessoryInstanceBackpack != null) Destroy(previewAccessoryInstanceBackpack);

            // 확정 장착된 장신구의 Renderer를 활성화
            if (confirmedAccessoryInstanceHat != null)
            {
                confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>().Renderer.enabled = true;
            }
            if (confirmedAccessoryInstanceBackpack != null)
            {
                confirmedAccessoryInstanceBackpack.GetComponent<AccessoryLogic>().Renderer.enabled = true;
            }
        }

        private void ConfirmAccessory()
        {
            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            if (isHat)
            {
                if (previewAccessoryInstanceHat == null) return;

                // 기존 확정 장착 해제
                if (confirmedAccessoryInstanceHat != null)
                {
                    Destroy(confirmedAccessoryInstanceHat);
                }

                // 미리보기 장신구를 확정 착용으로 전환
                confirmedAccessoryInstanceHat = previewAccessoryInstanceHat;
                previewAccessoryInstanceHat = null;

                AccessoryLogic accessoryLogic = confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>();
                accessoryWearLogic.Attach(accessoryLogic);
                confirmedAccessoryInstanceHat.transform.SetParent(character.transform, false);
            }
            else
            {
                if (previewAccessoryInstanceBackpack == null) return;

                // 기존 확정 장착 해제
                if (confirmedAccessoryInstanceBackpack != null)
                {
                    Destroy(confirmedAccessoryInstanceBackpack);
                }

                // 미리보기 장신구를 확정 착용으로 전환
                confirmedAccessoryInstanceBackpack = previewAccessoryInstanceBackpack;
                previewAccessoryInstanceBackpack = null;

                AccessoryLogic accessoryLogic = confirmedAccessoryInstanceBackpack.GetComponent<AccessoryLogic>();
                accessoryWearLogic.Attach(accessoryLogic);
                confirmedAccessoryInstanceBackpack.transform.SetParent(character.transform, false);
            }
        }

        private void RemoveConfirmedAccessory()
        {
            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            if (isHat && confirmedAccessoryInstanceHat != null)
            {
                AccessoryLogic accessoryLogic = confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>();
                accessoryWearLogic.Detach(accessoryLogic);
                Destroy(confirmedAccessoryInstanceHat);
                confirmedAccessoryInstanceHat = null;
            }
            else if (!isHat && confirmedAccessoryInstanceBackpack != null)
            {
                AccessoryLogic accessoryLogic = confirmedAccessoryInstanceBackpack.GetComponent<AccessoryLogic>();
                accessoryWearLogic.Detach(accessoryLogic);
                Destroy(confirmedAccessoryInstanceBackpack);
                confirmedAccessoryInstanceBackpack = null;
            }
        }

        private void SelectNextAccessory()
        {
            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            // 기존 미리보기 장신구 제거 (독립적으로 관리)
            if (isHat && previewAccessoryInstanceHat != null)
            {
                Destroy(previewAccessoryInstanceHat);
                previewAccessoryInstanceHat = null;
            }
            else if (!isHat && previewAccessoryInstanceBackpack != null)
            {
                Destroy(previewAccessoryInstanceBackpack);
                previewAccessoryInstanceBackpack = null;
            }

            // 다음 장신구로 인덱스 이동 및 선택
            currentAccessoryIndex = (currentAccessoryIndex + 1) % accessories.Count;
            selectedAccessoryPrefab = accessories[currentAccessoryIndex];

            // 새로운 미리보기 장신구 표시 전 확정 장신구의 Renderer 상태 갱신
            UpdateConfirmedAccessoryVisibility();
            ShowAccessoryPreview();
        }

        private void ShowAccessoryPreview()
        {
            if (selectedAccessoryPrefab == null || character == null) return;

            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            // 기존 확정 장착 장신구가 있을 경우, 같은 유형일 때만 Renderer를 비활성화하여 미리보기 장신구가 보이도록 함
            if (isHat && confirmedAccessoryInstanceHat != null)
            {
                confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>().Renderer.enabled = false;
            }
            else if (!isHat && confirmedAccessoryInstanceBackpack != null)
            {
                confirmedAccessoryInstanceBackpack.GetComponent<AccessoryLogic>().Renderer.enabled = false;
            }

            // 미리보기 장신구 인스턴스를 캐릭터에 임시로 추가
            GameObject previewInstance = Instantiate(selectedAccessoryPrefab);
            previewInstance.transform.SetParent(character.transform, false);
            AccessoryLogic accessoryLogic = previewInstance.GetComponent<AccessoryLogic>();

            // 캐릭터 본과 장신구 본을 일치시켜 투명 문제 해결
            accessoryWearLogic.Attach(accessoryLogic);
            accessoryLogic.Renderer.enabled = true;

            // 미리보기 인스턴스를 해당 유형에 맞게 설정
            if (isHat)
            {
                previewAccessoryInstanceHat = previewInstance;
            }
            else
            {
                previewAccessoryInstanceBackpack = previewInstance;
            }
        }

        // 미리보기 장신구의 유형에 따라 확정 장착된 장신구의 Renderer 상태를 업데이트
        private void UpdateConfirmedAccessoryVisibility()
        {
            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            // 미리보기 장신구가 backpack일 경우 확정 착용된 hat 장신구의 Renderer를 활성화
            if (!isHat && confirmedAccessoryInstanceHat != null)
            {
                confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>().Renderer.enabled = true;
            }
            else if (isHat && confirmedAccessoryInstanceBackpack != null)
            {
                confirmedAccessoryInstanceBackpack.GetComponent<AccessoryLogic>().Renderer.enabled = true;
            }
        }
    }
}
