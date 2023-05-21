using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private Dictionary<string, EnemyBase> enemyDict = new();

    public void CreatEnemy(string metaId, Vector3 worldPos, Quaternion quaternion)
    {
        var enemy = GameObjectPool.Instance.GetFromPool<Enemy>(metaId);
        var metaModel = SoManager.Instance.GetEnemySo(metaId);
        var guid = GameHelper.GetGUID();
        if (metaModel == null)
        {
            Debug.LogError("不存在" + metaId + "的So文件");
        }
        else
        {
            var enemyModel = new EnemyModel(guid, metaModel);
            enemy.Init(enemyModel);
        }
        var transform = enemy.transform;
        transform.position = worldPos;
        transform.rotation = quaternion;
        if (!enemyDict.ContainsKey(guid))
        {
            enemyDict.Add(guid, enemy);
        }
    }

    public void ReturnEnemy(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
        if (enemyDict.ContainsKey(enemy.EnemyModel.id))
        {
            enemyDict.Remove(enemy.EnemyModel.id);
        }
        GameObjectPool.Instance.ReturnToPool(enemy);
    }

    public Enemy GetMinDistanceEnemy(Vector3 startPos)
    {
        if (enemyDict.Count < 0)
        {
            return null;
        }
        
        var distance = float.MaxValue;
        Enemy enemy = null;
        foreach (var item in enemyDict)
        {
            var temp = Vector3.Distance(startPos, item.Value.transform.position);
            if (!(temp < distance)) continue;
            distance = temp;
            enemy = (Enemy)item.Value;
        }
        return enemy;
    }
}
