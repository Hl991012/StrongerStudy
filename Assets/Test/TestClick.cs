using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestClick : MonoBehaviour
{
    private void Awake()
    {
        Debug.LogError(Camera.main.worldToCameraMatrix);
        Debug.LogError(Camera.main.worldToCameraMatrix*new Vector4(0,4,-3.464101f,1));
        Debug.LogError(Camera.main.WorldToViewportPoint(new Vector3(0, 4, -3.464101f)));
    }
}
