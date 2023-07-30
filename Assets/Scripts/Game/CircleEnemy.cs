using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : Circle
{
    private void Awake()
    {
        Init(new CircleModel(0.9f, Vector2.zero, 0, CircleModel.MoveType.None)); 
        transform.localScale = Vector3.one * CircleModel.diameter;
    }
}
