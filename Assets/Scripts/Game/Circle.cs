using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    
    public CircleModel CircleModel { get; private set; }
    
    protected RemoveableDictionary<string, Circle> curTriggerCircles = new();

    public void Init(CircleModel model)
    {
        if (model == null)
        {
            
        }
        CircleModel = model;
    }

    public virtual void Update() 
    {
        Move();
        
        if (curTriggerCircles.Count > 0)
        {
            foreach (var item in curTriggerCircles)
            {
                if (item.Value.CircleModel.diameter <= CircleModel.diameter)
                {
                    TakeIn(item.Value);
                }
                else
                {
                    item.Value.TakeIn(this);
                }
            }
            curTriggerCircles.RemoveMarkedItems();
        }
    }

    protected virtual void Move()
    {
        if(CircleModel == null || CircleModel.moveType == CircleModel.MoveType.None) return;
    }

    /// <summary>
    /// 吸收
    /// </summary>
    public void TakeIn(Circle circle)
    {
        if (circle.CircleModel.diameter > 0)
        {
            float takeInArea = 0;
            if (circle.CircleModel.diameter >= 0.005f)
            {
                takeInArea = Mathf.PI * Mathf.Pow(circle.CircleModel.diameter / 2, 2) - Mathf.PI * Mathf.Pow((circle.CircleModel.diameter / 2 - 0.005f), 2);
            }
            else
            {
                takeInArea = Mathf.PI * Mathf.Pow(circle.CircleModel.diameter / 2, 2);
            }

            var origArea = Mathf.PI * Mathf.Pow(CircleModel.diameter / 2, 2);
            var curArea = origArea + takeInArea;
            CircleModel.diameter = Mathf.Sqrt(curArea / Mathf.PI) * 2;
            transform.localScale = Vector3.one * CircleModel.diameter;
            circle.TakeOut();
            if (circle.CircleModel.diameter <= 0 && curTriggerCircles.ContainsKey(circle.CircleModel.id))
            {
                curTriggerCircles.Remove(circle.CircleModel.id);
            }
        }
    }

    /// <summary>
    /// 被吸收
    /// </summary>
    public void TakeOut()
    {
        CircleModel.diameter -= 0.005f;
        if (CircleModel.diameter <= 0)
        {
            CircleModel.diameter = 0;
            Death();
        }
        transform.localScale = Vector3.one * CircleModel.diameter;
    }

    protected virtual void Death()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            var circle = col.gameObject.GetComponent<Circle>();
            if (circle != null && !curTriggerCircles.ContainsKey(circle.CircleModel.id))
            {
                curTriggerCircles.Add(circle.CircleModel.id, circle);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            var circle = other.gameObject.GetComponent<Circle>();
            if (circle != null && curTriggerCircles.ContainsKey(circle.CircleModel.id))
            {
                curTriggerCircles.Remove(circle.CircleModel.id);
            }
        }
    }
}
