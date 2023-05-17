using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<EnemyBase> enemyDict = new();

    public void CreatEnemy(string id, int level)
    {
        var enemy = GameObjectPool.Instance.GetFromPool<Enemy>(id);
        var metaModel = SoManager.Instance.GetEnemySo(id);
        if (metaModel == null)
        {
            Debug.LogError("不存在" + id + "的So文件");
        }
        else
        {
            enemy.Init(new EnemyModel(level, metaModel));
        }
        enemy.transform.position = Vector3.zero;  
    }
}
