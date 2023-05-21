using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyModel
{
    //唯一标识符
    public string id;
    
    public int curHp;

    /// <summary>
    /// 上一次攻击的时间
    /// </summary>
    public float lastAttackTime;
    
    public EnemyDataSo metaModel;
    public EnemyModel(string id ,EnemyDataSo metaModel)
    {
        this.id = id;
        this.metaModel = metaModel;
        curHp = metaModel.maxHp;
    }
}
