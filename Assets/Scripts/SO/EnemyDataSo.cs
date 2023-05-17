using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnemyDataSo), menuName = "SO/" + nameof(EnemyDataSo), order = 0)]
public class EnemyDataSo : ScriptableObject
{
    public string id;
    public int maxHp;
    public float attackInterval;
    public float moveSpeed;
}
