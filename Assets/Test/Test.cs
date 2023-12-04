using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.LogError(111);
        Test11();
        Debug.LogError(333);
    }

    public async UniTask TestUni(CancellationToken cancellationToken = default)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(20), ignoreTimeScale:true, PlayerLoopTiming.Update, cancellationToken);
    }

    public void Test11()
    {
        var a = TestForget().GetAwaiter();
    }

    public async UniTask TestForget()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(10));
        Debug.LogError(222);
    }
    
}
