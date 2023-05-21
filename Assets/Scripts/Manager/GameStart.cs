using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Init();
    }

    private void Init()
    {
        WeaponManager.Instance.Init();
    }

    private void Save()
    {
        WeaponManager.Instance.Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            Save();
        }
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Save();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Save();
        }
    }
}
