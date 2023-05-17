using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<EnemyBase> enemyList = new();

    public void CreatEnemy(string id, Vector3 worldPos, Quaternion quaternion)
    {
        var enemy = GameObjectPool.Instance.GetFromPool<Enemy>(id);
        var metaModel = SoManager.Instance.GetEnemySo(id);
        if (metaModel == null)
        {
            Debug.LogError("不存在" + id + "的So文件");
        }
        else
        {
            enemy.Init(new EnemyModel(metaModel));
        }

        var transform = enemy.transform;
        transform.position = worldPos;
        transform.rotation = quaternion;
        enemyList.Add(enemy);
    }

    public Enemy GetMinDistanceEnemy(Vector3 startPos)
    {
        if (enemyList.Count < 0)
        {
            return null;
        }
        var minDis = float.MaxValue;
        Enemy enemy = null;
        foreach (var item in enemyList)
        {
            var temp = Vector3.Distance(startPos, item.transform.position);
            if (!(temp < minDis)) continue;
            minDis = temp;
            enemy = (Enemy)item;
        }
        return enemy;
    }
}
