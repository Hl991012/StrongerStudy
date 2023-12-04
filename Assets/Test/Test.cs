using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Enums;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class Test : MonoBehaviour
{
    public GameObject prefab;

    private CancellationTokenSource cancellationTokenSource = new ();

    private void OnEnable()
    {
        // if (cancellationTokenSource != null)
        // {
        //     cancellationTokenSource.Dispose();
        // }
        //
        // cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }

    private void OnDestroy()
    {
        
    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            DelayTest().Forget();
        }
        Debug.LogError(11111);
        Debug.LogError(2222);
        Debug.LogError(333);

        Transform a = null;
        Debug.LogError(a.position);
        Debug.LogError(1);
    }

    public void Start()
    {
        //DelayTest().Forget();
        // TestDefaultField testDefaultField = new TestDefaultField();
        // testDefaultField.a = 1;
        // Debug.LogError(JsonConvert.SerializeObject(testDefaultField));
        // string json = "{\"a\":1}";
        // var model = JsonConvert.DeserializeObject<TestDefaultField>(json);
        // Debug.LogError(model.a);
        // Debug.LogError(model.b);
    }

    public async UniTask Test11()
    {
        var a = UniTask.Lazy(DelayTest);;
        await a;
        await a;
        await a;
    }

    public async UniTask DelayTest()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5), DelayType.Realtime, PlayerLoopTiming.Update, cancellationTokenSource.Token);
        Debug.LogError(11111);
    }

    public async UniTask<int> TestUniTaskT(CancellationToken cancellationToken = default)
    {
        await UniTask.Delay(10);
        return 10;
    }

    public async UniTaskVoid LoadManyAsync()
    {
        // 并行加载.
        var (a, b, c) = await UniTask.WhenAll(
            LoadAsSprite("foo"),
            LoadAsSprite("bar"),
            LoadAsSprite("baz"));

        var d = await UniTask.WhenAny(
                LoadAsSprite("foo"),
                LoadAsSprite("bar"),
                LoadAsSprite("baz")
        );
    }

    async UniTask<Sprite> LoadAsSprite(string path)
    {
        var resource = await Resources.LoadAsync<Sprite>(path);
        return (resource as Sprite);
    }
}

public class HollowOutMask : Graphic, ICanvasRaycastFilter
{
    public RectTransform innerTrans;//镂空区域
    public RectTransform outerTrans;//背景区域
    
    
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return false;
    }
}

[Serializable]
public class TestDefaultField
{
    [JsonProperty("a")]
    public int a;

    [JsonProperty("b")] public int b { get; set; } = -1;
}

