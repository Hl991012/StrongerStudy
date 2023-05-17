using System.Collections;
using TMPro;
using UnityEngine;
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
    }

    private void Update()
    {
        ShowBar();

        if (Input.GetKeyDown(KeyCode.A))
        {
            asyncOperation.allowSceneActivation = true;
        }
    }

    private IEnumerator StartLoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        Debug.Log(asyncOperation.isDone);
        yield return asyncOperation;
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

    private int bar = 0;
    void ShowBar()
    {
        int theProgress = 0;
        if (asyncOperation == null)
        {
            return;
        }
        
        Debug.Log(asyncOperation.progress);
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
            //asyncOperation.allowSceneActivation = true;
        }
        

    }
}
