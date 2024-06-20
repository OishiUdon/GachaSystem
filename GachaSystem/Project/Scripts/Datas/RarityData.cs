using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���A���e�B���Ƃ̃f�[�^��ݒ肷��X�N���v�^�u���I�u�W�F�N�g
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

    //���A���e�B�̖��O
    public string RarityName { get { return rarityName; } }

    //���A���e�B�����I�����m��
    public float RarityProbabirity { get { return rarityProbabirity; } }

    //���A���e�B���̃J�[�h���X�g
    public List<CardData> CardDatas { get { return cardDatas; } }
}