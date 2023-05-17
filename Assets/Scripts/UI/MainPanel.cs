using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;

    private void Awake()
    {
        startGameBtn.onClick.AddListener(() =>
        {
            var worldPos = new Vector3(Random.Range(5, 6), 0, Random.Range(5, 6));
            EnemyManager.Instance.CreatEnemy("Enemy", worldPos, Quaternion.LookRotation(Vector3.zero - worldPos, Vector3.up));
        });
    }
}
