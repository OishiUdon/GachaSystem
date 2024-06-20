using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h���Ƃ̃f�[�^��ݒ肷��X�N���v�^�u���I�u�W�F�N�g
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


    //�J�[�h�̖��O
    public string CardName { get { return cardName; } }

    //�J�[�h�̃��A���e�B
    public CardRarityType CardRarity { get { return cardRarity; } }

    //�J�[�h�����I�����m��
    public float CardProbabirity { get { return cardProbabirity; } }

    //�J�[�h�̔ԍ�
    public int CardID { get { return cardID; } }

    //�J�[�h�̐���
    public string Desc { get { return desc; } }

    //�J�[�h�̉摜
    public Texture2D CardImage { get { return cardImage; } }
}


