using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MainWeaponSO), menuName = "SO/" + nameof(MainWeaponSO), order = 0)]
public class MainWeaponSO : ScriptableObject
{
    public string id;
    public string attackInterval;
}

public class CreatSO : Editor
{
    [MenuItem("SO/WeaponSO")]
    public static void CreatWeaponSO()
    {
        var weaponDate = ScriptableObject.CreateInstance<WeaponData>();
        AssetDatabase.CreateAsset(weaponDate, "Assets/Resources/So/WeaponSO.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [Serializable]
    public class WeaponData : ScriptableObject
    {
        public string id;
        public string attackInterval;
    }
    
    [MenuItem("SO/WeaponSO")]
    public static void CreatEnemySO()
    {
        var enemyData = ScriptableObject.CreateInstance<EnemyData>();
        AssetDatabase.CreateAsset(enemyData, "Assets/Resources/So/EnemyData.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [Serializable]
    public class EnemyData : ScriptableObject
    {
        public string id;
        public float attackInterval;
        public float moveSpeed;
    }
}
