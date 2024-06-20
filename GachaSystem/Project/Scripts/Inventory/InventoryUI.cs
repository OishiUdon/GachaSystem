using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// インベントリ(図鑑)のUIを設定するスクリプト
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private GameObject contentHolder;

    [SerializeField]
    private TextMeshProUGUI countText;

    [SerializeField]
    private TextMeshProUGUI descCardNameText;
    [SerializeField]
    private TextMeshProUGUI descCardDataText;

    [SerializeField]
    private Image descCardImage;

    /// <summary>
    /// インベントリの中身を更新する
    /// </summary>
    /// <param name="inventory">更新するインベントリ</param>
    public void UpdateUI(Inventory inventory)
    {
        descCardNameText.text = null;
        descCardDataText.text = null;

        //インベントリにあるボタンの数
        int currentButtonCount = contentHolder.transform.childCount;

        //現在所持しているカードの種類数
        int currentCardCount = inventory.CardDataList.Count;

        if (currentButtonCount < currentCardCount)
        {
            //新しく追加されたカードの種類数を求める
            int num = currentCardCount - currentButtonCount;

            //新しく増えた数だけボタンを追加する
            for (int i = 0; i < num; i++)
            {
                GameObject newButtonObject = Instantiate(buttonPrefab);
                newButtonObject.transform.SetParent(contentHolder.transform, false);
            }
        }
        else if (currentButtonCount > currentCardCount)
        {
            //カードの種類が被らないように数を修正する
            for (int i = currentButtonCount - 1; i > currentCardCount - 1; i--)
            {
                Destroy(contentHolder.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < currentCardCount; i++)
        {
            CardData cardData = inventory.CardDataList[i];
            GameObject buttonObject = contentHolder.transform.GetChild(i).gameObject;
            TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = cardData.CardRarity + "：" + cardData.CardName;

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnClickCardButton(buttonObject.transform.GetSiblingIndex(), inventory));
        }
    }

    /// <summary>
    /// インベントリ内のカードがクリックされた際の処理を行う
    /// </summary>
    /// <param name="index">カードの番号</param>
    /// <param name="inventory">対象のインベントリ</param>
    void OnClickCardButton(int index, Inventory inventory)
    {
        descCardImage.gameObject.SetActive(true);

        descCardNameText.text = inventory.CardDataList[index].name;
        descCardDataText.text = inventory.CardDataList[index].Desc;
        if (inventory.CardDataList[index].CardImage != null)
        {
            Texture2D texture = inventory.CardDataList[index].CardImage;
            descCardImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }

    /// <summary>
    /// インベントリが開かれた際の処理を行う
    /// </summary>
    /// <param name="currentCount">インベントリ内の種類数</param>
    /// <param name="allCount">全てのカードの種類数</param>
    public void OpenInventoryUI(int currentCount,int allCount)
    {
        inventoryPanel.SetActive(true);
        countText.text = "Character：" + currentCount + "/" + allCount;
    }

    /// <summary>
    /// インベントリが閉じられた際の処理を行う
    /// </summary>
    public void CloseInventoryUI()
    {
        inventoryPanel.SetActive(false);
        descCardImage.gameObject.SetActive(false);
    }

}
