using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���I�e�[�u���̃f�[�^��ݒ肷��X�N���v�^�u���I�u�W�F�N�g
/// </summary>
[CreateAssetMenu(fileName = "TableData", menuName = "Custom Datas/TableData")]
public class TableData : ScriptableObject
{
    [SerializeField]
    private List<RarityData> rarityDatas = null;

    //���̃f�[�^�������A���e�B�f�[�^
    public List<RarityData> RarityDatas { get { return rarityDatas; } }
}
