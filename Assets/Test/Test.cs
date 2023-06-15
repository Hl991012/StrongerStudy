using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button startSpeakBtn;
    [SerializeField] private TextMeshProUGUI result;

    private static AndroidJavaClass unityPlayer;
    private AndroidJavaObject curActivity;
    private void Awake()
    {
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        startSpeakBtn.onClick.AddListener(StartSpeak);
    }

    private void StartSpeak()
    {
        Debug.LogError("点击按钮");
        curActivity.Call("startSpeak");
    }

    public void ResultBack(string result)
    {
        this.result.text = result;
    }
}
