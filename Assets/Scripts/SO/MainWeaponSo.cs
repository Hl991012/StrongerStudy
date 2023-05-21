using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MainWeaponSo), menuName = "SO/" + nameof(MainWeaponSo), order = 0)]
public class MainWeaponSo : ScriptableObject
{
    public string id;
    //攻击间隔
    public float[] attackInterval;
    //攻击范围
    public float attackRange;
    //场内升级对应的能量石
    public int[] levelToEnergyStone;

    public int[] levelToAttackDamage;

    public float LevelToAttackInterval(int level)
    {
        if (level >= attackInterval.Length)
        {
            return attackInterval[0];
        }

        return attackInterval[level];
    }
    
    public int LevelToAttackDamage(int level)
    {
        if (level >= levelToAttackDamage.Length)
        {
            return levelToAttackDamage[0];
        }

        return levelToAttackDamage[level];
    }
}

[Serializable]
public class MainWeaponModel
{
    public int level = 0;
    public bool isUnlocked = false;
    
    [JsonIgnore]
    public int curEnergyStone;

    [JsonIgnore] public float lastAttackTime = 0;
    
    [JsonIgnore]
    public MainWeaponSo metaModel;
}
