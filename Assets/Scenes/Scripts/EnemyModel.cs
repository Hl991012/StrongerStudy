using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyModel
{
    public int level;
    public EnemyDataSo metaModel;

    public EnemyModel(int level, EnemyDataSo metaModel)
    {
        this.level = level;
        this.metaModel = metaModel;
    }
}
