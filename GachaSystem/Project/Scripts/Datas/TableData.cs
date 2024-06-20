using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽選テーブルのデータを設定するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "TableData", menuName = "Custom Datas/TableData")]
public class TableData : ScriptableObject
{
    [SerializeField]
    private List<RarityData> rarityDatas = null;

    //このデータが持つレアリティデータ
    public List<RarityData> RarityDatas { get { return rarityDatas; } }
}
