using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleModel
{
    public enum MoveType
    {
        None,
        UpAndDown,
        LeftAndRight,
        Line,
        Circle,
    }
    
    /// <summary>
    /// 唯一标识
    /// </summary>
    public string id;
    public float diameter;
    public Vector2 curPos;
    public float moveSpeed;
    public MoveType moveType;

    public CircleModel(float diameter, Vector2 pos, float moveSpeed, MoveType moveType)
    {
        id = System.Guid.NewGuid().ToString();
        this.diameter = diameter;
        this.curPos = pos;
        this.moveSpeed = moveSpeed;
        this.moveType = moveType;
    }
}
