using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 他スクリプトの情報を統合しメインで動くスクリプト
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
    /// ガチャを引く処理を行う
    /// </summary>
    void OnPick()
    {
        //ガチャを回す
        lootManager.TotalResult();

        //引いたカードが取得したカードリストに含まれているかどうか
        if (inventory.CardDataList.Contains(lootManager.AddListMember))
        {
            newObtainCardText.text = null; //new!の文字を出さない
        }
        else
        {
            newObtainCardText.text = "new!"; //new!の文字を出す

            //インベントリのリストに新しく出たカードを追加する
            inventory.CardDataList.Add(lootManager.AddListMember);
        }
    }

    /// <summary>
    /// インベントリを開く際の処理を行う
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
    /// インベントリを閉じる際の処理を行う
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