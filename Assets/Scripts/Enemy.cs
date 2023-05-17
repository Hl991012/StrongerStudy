using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    private enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Death,
    }

    private EnemyState curState;

    public override void Init(EnemyModel model)
    {
        base.Init(model);
        ChangeState(EnemyState.Move);
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (EnemyModel.curHp <= 0)
        {
            ChangeState(EnemyState.Death);
            return;
        }
        
        switch (curState)
        {
            case EnemyState.Idle:
                //敌人在攻击范围内，然后在CD时间内才会进入Idle
                if (Time.time - EnemyModel.lastAttackTime > EnemyModel.metaModel.attackInterval)
                {
                    ChangeState(EnemyState.Attack);
                }
                break;
            case EnemyState.Move:
                transform.Translate(Vector3.forward * (Time.deltaTime * EnemyModel.metaModel.moveSpeed), Space.Self);

                //敌人在攻击范围外
                if (Vector3.Distance(Vector3.zero, transform.position) <= 2)
                {
                    ChangeState(EnemyState.Idle);
                }
                break;
            case EnemyState.Attack:
                //玩家在Idle状态下会切换到攻击
                if (Time.time - EnemyModel.lastAttackTime < EnemyModel.metaModel.attackInterval)
                {
                    ChangeState(EnemyState.Idle);
                }
                break;
        }
    }

    private void ChangeState(EnemyState state)
    {
        if (state == curState)
        {
            return;
        }
        curState = state;
        switch (curState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Move:
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Death:
                GameObjectPool.Instance.ReturnToPool(this);
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        EnemyModel.lastAttackTime = Time.time;
        Debug.LogError("攻击");
    }
}
