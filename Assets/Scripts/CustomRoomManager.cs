using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Supercyan.FreeSample
{
    public class CharacterCustomizationRoomManager : MonoBehaviour
    {
        // �̱��� �ν��Ͻ� ����
        public static CharacterCustomizationRoomManager Instance { get; private set; }

        [SerializeField] private Button accessoryConfirmButton;        // ��ű� Ȯ�� ���� ��ư
        [SerializeField] private Button accessoryRemoveButton;         // ��ű� ���� ��ư
        [SerializeField] private Button accessoryNextButton;           // ���� ��ű� ���� ��ư

        private CharacterControl character;                            // Player �±׸� ���� ĳ���� ����
        private AccessoryWearLogic accessoryWearLogic;                 // ĳ������ ��ű� ���� ����

        private List<GameObject> accessories;                          // ��ű� ���
        private int currentAccessoryIndex = 0;                         // ���� ���õ� ��ű� �ε���
        private GameObject selectedAccessoryPrefab;                    // ���� ���õ� ��ű� ������

        private GameObject previewAccessoryInstanceHat;                // �̸����� hat ��ű� �ν��Ͻ�
        private GameObject previewAccessoryInstanceBackpack;           // �̸����� backpack ��ű� �ν��Ͻ�
        private GameObject confirmedAccessoryInstanceHat;              // Ȯ�� ����� hat ��ű� �ν��Ͻ�
        private GameObject confirmedAccessoryInstanceBackpack;         // Ȯ�� ����� backpack ��ű� �ν��Ͻ�

        private void Awake()
        {
            // �̱��� �ν��Ͻ� ����
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // �ν��Ͻ� �ߺ� ����
                return;
            }
        }

        private void Start()
        {
            character = CharacterControl.Instance;
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

            accessoryConfirmButton.onClick.AddListener(ConfirmAccessory);
            accessoryRemoveButton.onClick.AddListener(RemoveConfirmedAccessory);
            accessoryNextButton.onClick.AddListener(SelectNextAccessory);

            selectedAccessoryPrefab = accessories.Count > 0 ? accessories[0] : null;
            ShowAccessoryPreview();
        }

        // �̸����� ��ű��� �����ϰ�, Ȯ�� ������ ��ű��� Renderer�� Ȱ��ȭ�ϴ� �޼���
        public void DeletePreviewAccessories()
        {
            // �̸����� ��ű� ����
            if (previewAccessoryInstanceHat != null) Destroy(previewAccessoryInstanceHat);
            if (previewAccessoryInstanceBackpack != null) Destroy(previewAccessoryInstanceBackpack);

            // Ȯ�� ������ ��ű��� Renderer�� Ȱ��ȭ
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

                // ���� Ȯ�� ���� ����
                if (confirmedAccessoryInstanceHat != null)
                {
                    Destroy(confirmedAccessoryInstanceHat);
                }

                // �̸����� ��ű��� Ȯ�� �������� ��ȯ
                confirmedAccessoryInstanceHat = previewAccessoryInstanceHat;
                previewAccessoryInstanceHat = null;

                AccessoryLogic accessoryLogic = confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>();
                accessoryWearLogic.Attach(accessoryLogic);
                confirmedAccessoryInstanceHat.transform.SetParent(character.transform, false);
            }
            else
            {
                if (previewAccessoryInstanceBackpack == null) return;

                // ���� Ȯ�� ���� ����
                if (confirmedAccessoryInstanceBackpack != null)
                {
                    Destroy(confirmedAccessoryInstanceBackpack);
                }

                // �̸����� ��ű��� Ȯ�� �������� ��ȯ
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

            // ���� �̸����� ��ű� ���� (���������� ����)
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

            // ���� ��ű��� �ε��� �̵� �� ����
            currentAccessoryIndex = (currentAccessoryIndex + 1) % accessories.Count;
            selectedAccessoryPrefab = accessories[currentAccessoryIndex];

            // ���ο� �̸����� ��ű� ǥ�� �� Ȯ�� ��ű��� Renderer ���� ����
            UpdateConfirmedAccessoryVisibility();
            ShowAccessoryPreview();
        }

        private void ShowAccessoryPreview()
        {
            if (selectedAccessoryPrefab == null || character == null) return;

            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            // ���� Ȯ�� ���� ��ű��� ���� ���, ���� ������ ���� Renderer�� ��Ȱ��ȭ�Ͽ� �̸����� ��ű��� ���̵��� ��
            if (isHat && confirmedAccessoryInstanceHat != null)
            {
                confirmedAccessoryInstanceHat.GetComponent<AccessoryLogic>().Renderer.enabled = false;
            }
            else if (!isHat && confirmedAccessoryInstanceBackpack != null)
            {
                confirmedAccessoryInstanceBackpack.GetComponent<AccessoryLogic>().Renderer.enabled = false;
            }

            // �̸����� ��ű� �ν��Ͻ��� ĳ���Ϳ� �ӽ÷� �߰�
            GameObject previewInstance = Instantiate(selectedAccessoryPrefab);
            previewInstance.transform.SetParent(character.transform, false);
            AccessoryLogic accessoryLogic = previewInstance.GetComponent<AccessoryLogic>();

            // ĳ���� ���� ��ű� ���� ��ġ���� ���� ���� �ذ�
            accessoryWearLogic.Attach(accessoryLogic);
            accessoryLogic.Renderer.enabled = true;

            // �̸����� �ν��Ͻ��� �ش� ������ �°� ����
            if (isHat)
            {
                previewAccessoryInstanceHat = previewInstance;
            }
            else
            {
                previewAccessoryInstanceBackpack = previewInstance;
            }
        }

        // �̸����� ��ű��� ������ ���� Ȯ�� ������ ��ű��� Renderer ���¸� ������Ʈ
        private void UpdateConfirmedAccessoryVisibility()
        {
            bool isHat = selectedAccessoryPrefab.name.Contains("hat");

            // �̸����� ��ű��� backpack�� ��� Ȯ�� ����� hat ��ű��� Renderer�� Ȱ��ȭ
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
