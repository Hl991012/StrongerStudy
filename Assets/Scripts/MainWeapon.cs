using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    private void Update()
    { 
        var targetEnemy = EnemyManager.Instance.GetMinDistanceEnemy(transform.position);
        if (targetEnemy != null)
        {
            
        }
    }
}
