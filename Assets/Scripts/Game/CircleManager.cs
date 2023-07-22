using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CircleManager : Singleton<CircleManager>
{
    public Dictionary<string, Circle> AllCirclesDict { get; private set; } = new();

    /// <summary>
    /// 创建圆形
    /// </summary>
    public async UniTask CreatCircle()
    {
         //await Addressables.InstantiateAsync()
    }
    
}
