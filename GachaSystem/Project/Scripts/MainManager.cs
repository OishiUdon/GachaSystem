using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���X�N���v�g�̏��𓝍������C���œ����X�N���v�g
/// </summary>
public class MainManager : MonoBehaviour
{
    private Inventory inventory;
    private InventoryUI inventoryUI;
    private LootManager lootManager;

    [SerializeField]
    private Button pickButton;

    [SerializeField]
    private Button openInventoryButton;

    [SerializeField]
    private Button closeInventoryButton;

    [SerializeField]
    private TextMeshProUGUI newObtainCardText;

    public bool pb_pick { get; private set; }
    public bool pb_open { get; private set; }
    public bool pb_close { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        pickButton.onClick.AddListener(() => pb_pick = true);
        openInventoryButton.onClick.AddListener(() => pb_open = true);
        closeInventoryButton.onClick.AddListener(() => pb_close = true);

        inventory = new Inventory();
        inventoryUI = GetComponentInChildren<InventoryUI>();
        lootManager = GetComponentInChildren<LootManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pb_pick)
        {
            OnPick();
        }

        if (pb_open)
        {
            OpenInventory();
        }
        if (pb_close)
        {
            CloseInventory();
        }
    }

    private void LateUpdate()
    {
        pb_pick = false;
        pb_open = false;
        pb_close = false;
    }

    /// <summary>
    /// �K�`���������������s��
    /// </summary>
    void OnPick()
    {
        //�K�`������
        lootManager.TotalResult();

        //�������J�[�h���擾�����J�[�h���X�g�Ɋ܂܂�Ă��邩�ǂ���
        if (inventory.CardDataList.Contains(lootManager.AddListMember))
        {
            newObtainCardText.text = null; //new!�̕������o���Ȃ�
        }
        else
        {
            newObtainCardText.text = "new!"; //new!�̕������o��

            //�C���x���g���̃��X�g�ɐV�����o���J�[�h��ǉ�����
            inventory.CardDataList.Add(lootManager.AddListMember);
        }
    }

    /// <summary>
    /// �C���x���g�����J���ۂ̏������s��
    /// </summary>
    private void OpenInventory()
    {
        pickButton.gameObject.SetActive(false);
        openInventoryButton.gameObject.SetActive(false);
        lootManager.ResultImageObject.SetActive(false);

        inventoryUI.OpenInventoryUI(inventory.CardDataList.Count, lootManager.AllCardCount);

        inventory.SortCardList();
        inventoryUI.UpdateUI(inventory);
    }

    /// <summary>
    /// �C���x���g�������ۂ̏������s��
    /// </summary>
    private void CloseInventory()
    {
        openInventoryButton.gameObject.SetActive(true);
        inventoryUI.CloseInventoryUI();

        pickButton.gameObject.SetActive(true);
        if (inventory.CardDataList.Count > 0)
        {
            lootManager.ResultImageObject.SetActive(true);
        }
    }
}