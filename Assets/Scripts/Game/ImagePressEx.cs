using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImagePressEx : Image, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    protected override void Awake()
    {
        base.Awake();
        alphaHitTestMinimumThreshold = 0.1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.GetComponent<RectTransform>(), 
            eventData.pressPosition, null, 
            out var localPos);
        InputManager.Instance.dir = localPos;
        InputManager.Instance.OnInputDirStateChanged?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.Instance.OnInputDirStateChanged?.Invoke(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.GetComponent<RectTransform>(), 
            eventData.position, null, 
            out var localPos);
        InputManager.Instance.dir = localPos;
        InputManager.Instance.OnInputDirStateChanged?.Invoke(true);
    }
}
