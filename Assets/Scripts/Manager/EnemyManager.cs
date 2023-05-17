using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<EnemyBase> enemyDict = new();

    public void CreatEnemy(string id)
    {
        var enemy = GameObjectPool.Instance.GetFromPool<EnemyBase>(id);
    }
}
