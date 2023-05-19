using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    private MainWeaponModel mainWeaponModel = new MainWeaponModel();
    
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
        
        var targetEnemy = EnemyManager.Instance.GetMinDistanceEnemy(transform.position);
        if(targetEnemy == null) return;
        
        if (targetEnemy != null)
        {
            Debug.Log("开始攻击");
            mainWeaponModel.lastAttackTime = Time.time;
        }
    }
}
