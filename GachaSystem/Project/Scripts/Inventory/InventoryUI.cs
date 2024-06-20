using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �C���x���g��(�}��)��UI��ݒ肷��X�N���v�g
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
    /// �C���x���g���̒��g���X�V����
    /// </summary>
    /// <param name="inventory">�X�V����C���x���g��</param>
    public void UpdateUI(Inventory inventory)
    {
        descCardNameText.text = null;
        descCardDataText.text = null;

        //�C���x���g���ɂ���{�^���̐�
        int currentButtonCount = contentHolder.transform.childCount;

        //���ݏ������Ă���J�[�h�̎�ސ�
        int currentCardCount = inventory.CardDataList.Count;

        if (currentButtonCount < currentCardCount)
        {
            //�V�����ǉ����ꂽ�J�[�h�̎�ސ������߂�
            int num = currentCardCount - currentButtonCount;

            //�V�����������������{�^����ǉ�����
            for (int i = 0; i < num; i++)
            {
                GameObject newButtonObject = Instantiate(buttonPrefab);
                newButtonObject.transform.SetParent(contentHolder.transform, false);
            }
        }
        else if (currentButtonCount > currentCardCount)
        {
            //�J�[�h�̎�ނ����Ȃ��悤�ɐ����C������
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

            buttonText.text = cardData.CardRarity + "�F" + cardData.CardName;

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnClickCardButton(buttonObject.transform.GetSiblingIndex(), inventory));
        }
    }

    /// <summary>
    /// �C���x���g�����̃J�[�h���N���b�N���ꂽ�ۂ̏������s��
    /// </summary>
    /// <param name="index">�J�[�h�̔ԍ�</param>
    /// <param name="inventory">�Ώۂ̃C���x���g��</param>
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
    /// �C���x���g�����J���ꂽ�ۂ̏������s��
    /// </summary>
    /// <param name="currentCount">�C���x���g�����̎�ސ�</param>
    /// <param name="allCount">�S�ẴJ�[�h�̎�ސ�</param>
    public void OpenInventoryUI(int currentCount,int allCount)
    {
        inventoryPanel.SetActive(true);
        countText.text = "Character�F" + currentCount + "/" + allCount;
    }

    /// <summary>
    /// �C���x���g��������ꂽ�ۂ̏������s��
    /// </summary>
    public void CloseInventoryUI()
    {
        inventoryPanel.SetActive(false);
        descCardImage.gameObject.SetActive(false);
    }

}
