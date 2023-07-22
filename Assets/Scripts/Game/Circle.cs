using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] 
    
    public CircleModel CircleModel { get; private set; }

    public void Init(CircleModel model)
    {
        if (model == null)
        {
            
        }
        CircleModel = model;
    }

    public void Update() 
    {
        Move();
    }

    private void Move()
    {
        if(CircleModel == null || CircleModel.moveType == CircleModel.MoveType.None) return;
    }

    /// <summary>
    /// 吸收
    /// </summary>
    private void TakeIn()
    {
        
    }

    /// <summary>
    /// 被吸收
    /// </summary>
    private void TakeOut()
    {
        
    }
}
