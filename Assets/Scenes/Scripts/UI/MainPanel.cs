using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;

    private void Awake()
    {
        startGameBtn.onClick.AddListener(() =>
        {
            EnemyManager.Instance.CreatEnemy("Enemy", 1);
        });
    }
}
