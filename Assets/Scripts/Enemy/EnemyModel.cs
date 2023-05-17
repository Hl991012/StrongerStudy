using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyModel
{
    public int curHp;

    /// <summary>
    /// 上一次攻击的时间
    /// </summary>
    public float lastAttackTime;
    
    public EnemyDataSo metaModel;
    public EnemyModel(EnemyDataSo metaModel)
    {
        this.metaModel = metaModel;
        curHp = metaModel.maxHp;
    }
}
