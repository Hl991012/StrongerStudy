using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class CircleManager : Singleton<CircleManager>
{
    public Dictionary<string, Circle> AllCirclesDict { get; private set; } = new();

    /// <summary>
    /// 创建圆形
    /// </summary>
    public async UniTask<Circle> CreatCircle(float radius, Vector2 pos)
    {
        var enemyPrefab = await Addressables.LoadAssetAsync<GameObject>("Enemy");
        var obj = Object.Instantiate(enemyPrefab);
        var circle = obj.GetComponent<Circle>();
        var circleModel = new CircleModel(radius, pos, 0, CircleModel.MoveType.None);
        circle.Init(circleModel);
        var transform = circle.transform;
        transform.localScale = Vector2.one * radius;
        transform.position = pos;
        return circle;
    }
    
}
