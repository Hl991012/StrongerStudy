using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    
    private MainWeaponModel mainWeaponModel;

    private EnemyBase attackTarget;

    private void Awake()
    {
        mainWeaponModel = new MainWeaponModel
        {
            metaModel = SoManager.Instance.mainWeaponSo
        };
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Time.time < mainWeaponModel.lastAttackTime + mainWeaponModel.metaModel.LevelToAttackInterval(mainWeaponModel.level))
        {
            return;
        }
        
        if (attackTarget == null)
        {
            attackTarget = EnemyManager.Instance.GetMinDistanceEnemy(transform.position);
        }

        if(attackTarget == null) return;

        var distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        if (distance < mainWeaponModel.metaModel.attackRange)
        {
            Debug.LogError("在攻击范围内，主武器开始攻击");
            mainWeaponModel.lastAttackTime = Time.time;
            TrackMissiles trackMissiles = GameObjectPool.Instance.GetFromPool<TrackMissiles>("TrackMissiles");
            trackMissiles.transform.position = firePos.position;
            trackMissiles.Init(attackTarget);
        }
    }
}
