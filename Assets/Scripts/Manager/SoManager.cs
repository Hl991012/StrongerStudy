using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SoManager), menuName = "SO/" + nameof(SoManager), order = 1)]
public class SoManager : ScriptableObject
{
    #region 单例模式

    private static SoManager instance;

    public static SoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<SoManager>("So/SOCenter");
            }

            return instance;
        }
    }

    private SoManager() { }

    #endregion

    public DefaultConfigSo defaultConfigSo;

    public MainWeaponSo mainWeaponSo;
    
    [SerializeField] private SerializableDictionary<string, EnemyDataSo> enemyDict = new();
    [SerializeField] private SerializableDictionary<string, MainWeaponSo> mainWeaponSoDict = new();

    public EnemyDataSo GetEnemySo(string id)
    {
        enemyDict.TryGetValue(id, out var enemyDataSo);
        return enemyDataSo;
    }

    public MainWeaponSo GetMainWeaponSo(string id)
    {
        mainWeaponSoDict.TryGetValue(id, out var mainWeaponSo);
        return mainWeaponSo;
    }
}
