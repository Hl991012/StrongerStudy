using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMissiles : MonoBehaviour, IRecyclable
{
    private EnemyBase trackTarget;
    public string ID { get; set; } = "TrackMissiles";

    private bool startFlyToTarget = false;

    public void Init(EnemyBase enemy)
    {
        trackTarget = enemy;
    }
    
    private void Update()
    {
        if(trackTarget == null) return;

        if (!startFlyToTarget && transform.position.y < 10)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 6, Space.World);
        }
        else
        {
            startFlyToTarget = true;
            transform.position = Vector3.Lerp(transform.position, trackTarget.transform.position, 0.02f);
            if (Vector3.Distance(transform.position, trackTarget.transform.position) < 0.3f)
            {
                Debug.LogError("开始爆炸");
                trackTarget.BeAttacked(10);   
                trackTarget = null;
                GameObjectPool.Instance.ReturnToPool(this);
                gameObject.SetActive(false);
            }
        }
    }
}
