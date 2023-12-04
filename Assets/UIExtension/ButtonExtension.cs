using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonExtension : Button
{
    public bool singleClickEnabled = true;
    public bool doubleClickEnabled = false;
    public float doubleClickTime = 0.3f;
    private float lastClickTime = float.NegativeInfinity;
    private int thisTimeClickCount = 0;
    
    [FormerlySerializedAs("onDoubleClick")]
    [SerializeField]
    private ButtonClickedEvent doubleClickEvent = new ();
    
    public ButtonClickedEvent onDoubleClick
    {
        get => doubleClickEvent;
        set => doubleClickEvent = value;
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        if(!IsActive() && !interactable) 
            return;
        
        thisTimeClickCount++;
        switch (thisTimeClickCount)
        {
            case 1:
                lastClickTime = Time.unscaledTime;
                if (singleClickEnabled)
                {
                    UISystemProfilerApi.AddMarker("Button.onClick", this);
                    onClick?.Invoke();
                    Debug.LogError("单机1");   
                }
                break;
            case 2:
                if (Time.realtimeSinceStartup - lastClickTime < doubleClickTime)
                {
                    if (doubleClickEnabled)
                    {
                        doubleClickEvent?.Invoke();
                        Debug.LogError("双击");
                    }
                    lastClickTime = float.NegativeInfinity;
                    thisTimeClickCount = 0;   
                }
                else
                {
                    if (singleClickEnabled)
                    {
                        base.OnPointerClick(eventData);
                        Debug.LogError("单机2");
                    }
                    lastClickTime = Time.unscaledTime;
                    thisTimeClickCount = 1;   
                }
                break;
        }
    }

    #region 长按相关

    public bool longPressEnabled = false;

    private float lastPressTime = 0;
    
    public float minPressTime = 0.5f;

    public ButtonClickedEvent longPressEvent = new();
    
    private bool isPressing = false;
    
    public ButtonClickedEvent onLongPress
    {
        get => longPressEvent;
        set => longPressEvent = value;
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (longPressEnabled)
        {
            lastPressTime = Time.unscaledTime;   
        }
    }
    
    //override OnMo

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (Time.unscaledTime - lastPressTime >= minPressTime)
        {
            onLongPress?.Invoke();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    private void Update()
    {
        
    }

    #endregion
}
