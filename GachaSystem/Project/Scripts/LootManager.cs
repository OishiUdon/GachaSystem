using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// �w�肳�ꂽ�f�[�^���烉���_���ɒ��I����X�N���v�g
/// </summary>
public class LootManager : MonoBehaviour
{
    [SerializeField]
    private TableData tableData;

    private RarityData resultRarityData = null;
    private CardData resultCardData = null;
    private int selectRarityNumber = 0;
    private int selectCardNumber = 0;

    private int allCardCount = 0;
    public int AllCardCount { get { return allCardCount; } }

    [SerializeField]
    private TextMeshProUGUI PickCountText;
    private int SelectCountValue = 0;

    [SerializeField]
    private TextMeshProUGUI PickResultText;

    [SerializeField]
    private Image resultImage;
    public GameObject ResultImageObject { get { return resultImage.gameObject; } }

    private CardData addListMember;
    public CardData AddListMember { get { return addListMember; } }


    private void Start()
    {
        for(int i= 0; i < tableData.RarityDatas.Count; i++)
        {
            allCardCount += tableData.RarityDatas[i].CardDatas.Count;
        }
    }

    /// <summary>
    /// �ݒ肳�ꂽ���[�g�e�[�u���̒����璊�I���s��
    /// </summary>
    public void CardSelect()
    {
        float[] item_rare = new float[tableData.RarityDatas.Count];
        for (int i = 0; i < tableData.RarityDatas.Count; i++)
        {
            item_rare[i] = tableData.RarityDatas[i].RarityProbabirity;
        }
        selectRarityNumber = (int)RandomSelect(item_rare);
        resultRarityData = tableData.RarityDatas[selectRarityNumber];

        float[] cardIndex = new float[resultRarityData.CardDatas.Count];
        for (int i = 0; i < resultRarityData.CardDatas.Count; i++)
        {
            cardIndex[i] = resultRarityData.CardDatas[i].CardProbabirity;
        }
        selectCardNumber = (int)RandomSelect(cardIndex);
        resultCardData = resultRarityData.CardDatas[selectCardNumber];

        SelectCountValue++;
    }

    /// <summary>
    /// �K�`�����s�������ʂ��Ăяo��
    /// </summary>
    public void TotalResult()
    {
        CardSelect();

        PickCountText.text = "Count�F" + SelectCountValue;

        Debug.Log(resultRarityData.RarityName + " : " + resultCardData.CardName);
        PickResultText.text = resultRarityData.RarityName + " : \n" + resultCardData.CardName;

        if (resultCardData != null)
        {
            resultImage.gameObject.SetActive(true);
            Texture2D texture = resultCardData.CardImage;
            resultImage.sprite = Sprite.Create(resultCardData.CardImage, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        addListMember = resultCardData;
    }

    /// <summary>
    /// �d�ݕt�����I���s���A�I�΂ꂽ�l��Ԃ�
    /// </summary>
    /// <param name="probs">���I���s�������m���̔z��</param>
    /// <returns></returns>
    private float RandomSelect(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }
}
