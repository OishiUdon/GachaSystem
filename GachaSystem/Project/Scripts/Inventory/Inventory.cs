using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インベントリ(図鑑)の中身を設定するスクリプト
/// </summary>
public class Inventory : MonoBehaviour
{
    private List<CardData> cardDataList = new List<CardData>();

    public List<CardData> CardDataList { get { return cardDataList; } }

    /// <summary>
    /// リスト内のカードをソートする
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
