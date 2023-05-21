using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnemyDataSo), menuName = "SO/" + nameof(EnemyDataSo), order = 0)]
public class EnemyDataSo : ScriptableObject
{
    public string metaId;
    public int maxHp;
    public int attackRange;
    public float attackInterval;
    public float moveSpeed;
}
