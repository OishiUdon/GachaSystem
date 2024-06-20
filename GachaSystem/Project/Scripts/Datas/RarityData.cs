using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レアリティごとのデータを設定するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "RarityData", menuName = "Custom Datas/RarityData")]
public class RarityData : ScriptableObject
{
    [SerializeField]
    private string rarityName = "None";

    [SerializeField]
    private float rarityProbabirity = 1;

    [SerializeField]
    private List<CardData> cardDatas = null;

    //レアリティの名前
    public string RarityName { get { return rarityName; } }

    //レアリティが抽選される確率
    public float RarityProbabirity { get { return rarityProbabirity; } }

    //レアリティ内のカードリスト
    public List<CardData> CardDatas { get { return cardDatas; } }
}