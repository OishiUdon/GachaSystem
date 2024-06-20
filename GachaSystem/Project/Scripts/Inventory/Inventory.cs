using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C���x���g��(�}��)�̒��g��ݒ肷��X�N���v�g
/// </summary>
public class Inventory : MonoBehaviour
{
    private List<CardData> cardDataList = new List<CardData>();

    public List<CardData> CardDataList { get { return cardDataList; } }

    /// <summary>
    /// ���X�g���̃J�[�h���\�[�g����
    /// </summary>
    public void SortCardList()
    {
        CardData temp;

        for (int h = 0; h < cardDataList.Count; h++)
        {
            int min_index = h;
            for (int i = h + 1; i < cardDataList.Count; i++)
            {
                if (cardDataList[min_index].CardID > cardDataList[i].CardID)
                {
                    min_index = i;
                }
            }
            temp = cardDataList[h];
            cardDataList[h] = cardDataList[min_index];
            cardDataList[min_index] = temp;
        }
    }
}
