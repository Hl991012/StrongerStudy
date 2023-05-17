using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IRecyclable
{
    public string ID { get; set; }

    public EnemyModel EnemyModel { get; private set; }

    public virtual void Init(EnemyModel model)
    {
        EnemyModel = model;
        ID = EnemyModel.metaModel.id;
    }
}
