using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SingleItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI index;
    
    private ScrollWheelEx scrollWheelEx;

    private Vector3[] worldCorners = new Vector3[4];

    public void Init(ScrollWheelEx scrollWheelEx)
    {
        this.scrollWheelEx = scrollWheelEx;
        scrollWheelEx.OnMove += OnMove;
    }

    private void OnDisable()
    {
        scrollWheelEx.OnMove -= OnMove;
    }

    private void OnMove()
    {
        transform.GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        if (IsFirst())
        {
            if (worldCorners[0].x > scrollWheelEx.ViewStart)
            {
                scrollWheelEx.AddFirst();
            }

            if (worldCorners[3].x < scrollWheelEx.ViewStart)
            {
                scrollWheelEx.RemoveFirst();
            }
        }

        if (IsLast())
        {
            if (worldCorners[0].x > scrollWheelEx.ViewEnd)
            {
                scrollWheelEx.RemoveLast();
            }

            if (worldCorners[3].x < scrollWheelEx.ViewEnd)
            {
                scrollWheelEx.AddLast();
            }
        }
    }

    private bool IsFirst() => transform.GetSiblingIndex() == 0;

    private bool IsLast() => transform.GetSiblingIndex() == transform.parent.childCount - 1;

    public void RefreshView(int index)
    {
        this.index.text = index.ToString();
    }
}
