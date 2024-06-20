using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードごとのデータを設定するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "CardData",menuName = "Custom Datas/CardData")]
public class CardData : ScriptableObject
{
    [SerializeField]
    private string cardName = "None";

    [SerializeField]
    private float cardProbabirity = 1;

    [SerializeField]
    private int cardID = 0;

    [SerializeField]
    [TextArea(5, 10)]
    private string desc = "None";

    [SerializeField]
    private Texture2D cardImage = null;

    public enum CardRarityType
    {
        None,
        N,
        R,
        SR,
        SSR
    }

    [SerializeField]
    private CardRarityType cardRarity;


    //カードの名前
    public string CardName { get { return cardName; } }

    //カードのレアリティ
    public CardRarityType CardRarity { get { return cardRarity; } }

    //カードが抽選される確率
    public float CardProbabirity { get { return cardProbabirity; } }

    //カードの番号
    public int CardID { get { return cardID; } }

    //カードの説明
    public string Desc { get { return desc; } }

    //カードの画像
    public Texture2D CardImage { get { return cardImage; } }
}


