using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAsync : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private Image progressSlider;
    
    private AsyncOperation asyncOperation;

    private Coroutine coroutine;

    private void Awake()
    {
        coroutine = StartCoroutine(StartLoadScene());
         SceneManager.LoadSceneAsync("GameScene").ToUniTask(Progress.Create<float>(
             (p) =>
         {
             progressSlider.fillAmount = p;
             progress.text = $"{p * 100:F2}%";
         }));

        
    }

    private void Update()
    {
        //ShowBar();
    }

    private IEnumerator StartLoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }

    private void OnDestroy()
    {
        //StopCoroutine(coroutine);
    }

    private int bar = 0;
    void ShowBar()
    {
        var theProgress = 0;
        if (asyncOperation == null)
        {
            return;
        }
        
        if (asyncOperation.progress < 0.9f)
        {
            theProgress = (int)asyncOperation.progress * 100;
        }
        else
        {
            theProgress = 100;
        }

        if (bar<theProgress)
        {
            bar++;
        }

        progress.text = "Loading..." + bar + "%";

        if (bar == 100)
        {
            asyncOperation.allowSceneActivation = true;
        }
        

    }
}
