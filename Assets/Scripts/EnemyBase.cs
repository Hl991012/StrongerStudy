using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IRecyclable
{
    public string ID { get; set; }

    private EnemyModel enemyModel;
    
    private void Awake()
    {
        
    }

    public void Init(EnemyModel model)
    {
        enemyModel = model;
        ID = enemyModel.metaModel.id;
    }
}
