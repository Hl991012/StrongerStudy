using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Threading;
using System.Xml.Serialization;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ThreadTest : MonoBehaviour
{
    // private Thread subThread;
    // private Thread mainThread;
    // [SerializeField] private RawImage Image;
    // [SerializeField] private Button button;
    private void Awake()
    {
        Debug.Log(SystemInfo.graphicsDeviceVendorID);
        // button.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
        // {
        //     Debug.LogError(111);
        // });

        // mainThread = Thread.CurrentThread;
        // ThreadStart threadStart = new ThreadStart(() =>
        // {
        //     Debug.LogError(subThread.ThreadState);
        //     for (int i = 0; i < 10; i++)
        //     {
        //         Thread.Sleep(1000);
        //         Debug.LogError("1111");
        //     }
        //     Debug.LogError("开启了一个子线程");
        // });
        // subThread = new Thread(threadStart)
        // {
        //     IsBackground = true
        // };
        // Debug.LogError(subThread.ThreadState);
        // subThread.Start();
        // //第一个参数为工作者线程，第二个参数为I/O线程
        // ThreadPool.SetMaxThreads(5, 2);

        // FunTest(() =>
        // {
        //     Debug.LogError(11111);
        // },
        //     () =>
        //     {
        //         Debug.LogError(222222);
        //     });

        // Debug.LogError(FunTest<int>(() => 10)?.Invoke());
        //
        // var d = Thread.AllocateNamedDataSlot("usename");
        // Thread.SetData(d, "usename");
        // Debug.LogError(Thread.GetData(d));
        // Thread thread = new(() =>
        // {
        //     Thread.SetData(d, "llll");
        //     Debug.LogError(Thread.GetData(d));
        // });
        // thread.Start();

        //四种启动多线程的方式
        // Task task = new Task(() => { });
        // task.Start();
        //
        // Task.Run(() => { });
        //
        // TaskFactory taskFactory1 = new TaskFactory();
        // taskFactory1.StartNew(() => { });
        //
        // TaskFactory taskFactory2 = Task.Factory;
        // taskFactory2.StartNew(() => { });
        //TestWebRequest();

        TestClasss testClasss = new TestClasss() { a =10};
        TestClasss testClass1 = testClasss;
        testClass1.a = 20;
        Debug.LogError(testClasss.a);
        TestClasss testClass2 = DeepCopy1<TestClasss>(testClasss);
        testClass2.a = 30;
        Debug.LogError(testClasss.a);
        TestClasss testClass3 = DeepCopy2<TestClasss>(testClasss);
        testClass3.a = 40;
        Debug.LogError(testClasss.a);
        TestClasss testClass4 = DeepCopy3<TestClasss>(testClasss);
        testClass4.a = 40;
        Debug.LogError(testClasss.a);
        TestClasss testClass5 = DeepCopy4<TestClasss>(testClasss);
        testClass5.a = 40;
        Debug.LogError(testClasss.a);
        TestClasss testClass6 = DeepCopy<TestClasss>(testClasss);
        testClass6.a = 40;
        Debug.LogError(testClasss.a);
        Debug.LogError(testClass6.a);

        var max = Mathf.Max(1, 1);
    }
    
    /// <summary>
    /// 方法封装多线程，实现两个委托顺序执行
    /// </summary>
    public void FunTest(Action action1, Action action2)
    {
        Thread thread = new Thread(() =>
        {
            action1?.Invoke();
            action2?.Invoke();
        });
        
        thread.Start();
    }

    /// <summary>
    /// 获得
    /// </summary>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Func<T> FunTest<T>(Func<T> action)
    {
        T t = default(T);
        Thread thread = new Thread(() =>
        {
            t = action.Invoke();
        });
        thread.Start();
        
        return new Func<T>(() =>
        {
            thread.Join();
            return t;
        });
    }

    public async void TestWebRequest()
    {
        var webRequest = UnityWebRequestTexture.GetTexture("https://lmg.jj20.com/up/allimg/tp09/210H51R3313N3-0-lp.jpg");
        var result = await webRequest.SendWebRequest();
        var tex = ((DownloadHandlerTexture)result.downloadHandler).texture;
        //Image.texture = tex;
    }

    //private CancellationToken m_token = new CancellationToken();
    //CancellationTokenSource.CreateLinkedTokenSource();
    
    public async void TestCancelToken(CancellationToken cancellationToken)
    {
        await UniTask.NextFrame(cancellationToken);
    }

    // public UniTask<int> WrapByUniTaskCompletionSource()
    // {
    //     var utcs = new UniTaskCompletionSource<int>();
    //     utcs.TrySetResult(utcs.GetResult());
    //     return utcs.Task;
    //
    // }
    private CancellationTokenSource CancellationToken = new CancellationTokenSource();
    public async UniTaskVoid Test()
    {
        // try
        // {
        //     await UniTask.Delay(TimeSpan.FromSeconds(10), DelayType.UnscaledDeltaTime,
        //         PlayerLoopTiming.Update, CancellationToken.Token);
        //     Debug.LogError("11111111");
        // }
        // catch (Exception e)
        // {
        //     Debug.LogError("异步被去取消了");
        // }
        
        
        // bool cancel = await UniTask.Delay(TimeSpan.FromSeconds(10), DelayType.UnscaledDeltaTime,
        //     PlayerLoopTiming.Update, CancellationToken.Token).SuppressCancellationThrow();
        // if (cancel)
        // {
        //     Debug.LogError(22222);
        // }
        // Debug.LogError("11111111");

        // await UniTaskAsyncEnumerable.EveryUpdate().ForEachAsync(_ =>
        // {
        //     Debug.LogError(11111);
        // });
    }


    private void OnEnable()
    {
        // CancellationToken?.Dispose();
        // CancellationToken = new CancellationTokenSource();
        // Test();
    }

    private void OnDisable()
    {
        CancellationToken.Cancel();
    }
    
    public T DeepCopy1<T>(T obj)
    {
        using MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(memoryStream ,obj);
        memoryStream.Seek(0, SeekOrigin.Begin);
        var t = (T)binaryFormatter.Deserialize(memoryStream);
        return t;
    }

    public T DeepCopy2<T>(T obj)
    {
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }
    
    public T DeepCopy3<T>(T obj)
    {
        return JsonUtility.FromJson<T>(JsonUtility.ToJson(obj));
    }

    public T DeepCopy4<T>(T obj)
    {
        using MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        xmlSerializer.Serialize(memoryStream ,obj);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return (T)xmlSerializer.Deserialize(memoryStream);
    }

    public T DeepCopy<T>(T obj)
    {
        if (obj is string || typeof(T).IsValueType)
        {
            return obj;
        }

        var instance = Activator.CreateInstance(obj.GetType());
        FieldInfo[] fieldInfos = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (var item in fieldInfos)
        {
            try
            {
                item.SetValue(instance, DeepCopy(item.GetValue(obj)));
            }
            catch (Exception e) { }
        }

        return (T)instance;
    }
    
    [Serializable]
    public class TestClasss
    {
        public int a = 10;
    }
}