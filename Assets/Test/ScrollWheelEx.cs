using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ScrollWheelEx : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform content;

    [SerializeField] private float space;

    private Vector3[] worldCorners = new Vector3[4];

    public Action OnMove = null;

    private int totalCount = 0;

    public float ViewStart { get; private set; }
    public float ViewEnd { get; private set; }

    private int firstShowIndex = 0;
    private int lastShowIndex = 0;

    private void Awake()
    {
        transform.GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        ViewStart = worldCorners[0].x;
        ViewEnd = worldCorners[3].x;
        
        //根据数据去生成
        totalCount = GetTotalCount();
        
        var firstItem =  GetSingleItem();
        firstItem.RefreshView(firstShowIndex);
        firstItem.transform.position = transform.position;
        AddFirst();
        AddLast();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        content.anchoredPosition += new Vector2(eventData.delta.x, 0) * 5;
        OnMove?.Invoke();
    }

    public void AddFirst()
    {
        firstShowIndex--;
        if (firstShowIndex < 0)
        {
            firstShowIndex = totalCount - 1;
        }
        var singleItem = GetSingleItem();
        var firstItemAnchoredPos = content.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        singleItem.transform.SetSiblingIndex(0);
        singleItem.RefreshView(firstShowIndex);
        singleItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(firstItemAnchoredPos.x - space - 100, 0);
    }
    
    public void RemoveFirst()
    {
        firstShowIndex++;
        if (firstShowIndex >= totalCount)
        {
            firstShowIndex = 0;
        }
        ReturnItem(content.GetChild(0).GetComponent<SingleItem>());
    }
    

    public void AddLast()
    {
        lastShowIndex++;
        if (lastShowIndex >= totalCount)
        {
            lastShowIndex = 0;
        }
        var lastItemAnchoredPos = content.GetChild(content.childCount - 1).GetComponent<RectTransform>().anchoredPosition;
        var singleItem = GetSingleItem();
        singleItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(lastItemAnchoredPos.x + space + 100, 0);
        singleItem.RefreshView(lastShowIndex);
    }

    public void RemoveLast()
    {
        lastShowIndex--;
        if (lastShowIndex < 0)
        {
            lastShowIndex = totalCount - 1;
        }
        ReturnItem(content.GetChild(content.childCount - 1).GetComponent<SingleItem>());
    }

    public abstract List<T> GetData<T>();
    public abstract int GetTotalCount();
    public abstract T GetDataByIndex<T>(int index);

    #region 对象池

    [SerializeField] private SingleItem prefab;
    [SerializeField] private Transform poolTrans;
    private readonly Queue<SingleItem> cellPool = new Queue<SingleItem>();

    private SingleItem GetSingleItem()
    {
        SingleItem singleItem = cellPool.Count <= 0 ? Instantiate(prefab) : cellPool.Dequeue();
        singleItem.gameObject.SetActive(true);
        singleItem.transform.SetParent(content);
        singleItem.Init(this);
        return singleItem;
    }

    private void ReturnItem(SingleItem trans)
    {
        if(trans == null) return;
        trans.gameObject.SetActive(false);
        trans.transform.SetParent(poolTrans);
        cellPool.Enqueue(trans);
    }
    #endregion
}
