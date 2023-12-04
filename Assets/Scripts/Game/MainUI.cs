using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Button spitBtn;
    [SerializeField] private Button pushBtn;
    private void Awake()
    {
        InputManager.Instance.OnInputDirStateChanged += OnInputDirStateChanged;
        
        spitBtn.onClick.AddListener(() =>
        {
            InputManager.Instance.OnClickSpitBtn?.Invoke();
        });
        
        pushBtn.onClick.AddListener(() =>
        {
            
        });
        
    }

    private void OnInputDirStateChanged(bool active)
    {
        spitBtn.gameObject.SetActive(active);
        pushBtn.gameObject.SetActive(active);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            spitBtn.onClick?.Invoke();
        }
    }
}
