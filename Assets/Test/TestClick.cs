using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestClick : MonoBehaviour
{
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private Button clickBtn;

    private bool temp = false;

    private void Awake()
    {
        clickBtn.onClick.AddListener(() =>
        {
            if (!temp)
            {
                m_rectTransform.sizeDelta = new Vector2(300, 100);
            }
            else
            {
                m_rectTransform.sizeDelta = new Vector2(100, 100);
            }

            temp = !temp;
        });
    }
}
