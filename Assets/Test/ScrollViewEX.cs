using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewEX : ScrollRect
{
    private enum Direction
    {
        Horizontal,
        Vertical,
    }

    private Direction curDirection;
    
    private Direction dragDirection;

    private ScrollViewEX parent;

    protected override void Awake()
    {
        base.Awake();
        if (parent == null)
        {
            parent = transform.parent.GetComponentInParent<ScrollViewEX>();
        }

        curDirection = this.horizontal ? Direction.Horizontal : Direction.Vertical;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        dragDirection = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y) ? 
            Direction.Horizontal : Direction.Vertical;
        if (parent != null && curDirection != dragDirection)
        {
            ExecuteEvents.Execute(parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
            return;
        }
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (parent != null && curDirection != dragDirection)
        {
            ExecuteEvents.Execute(parent.gameObject, eventData, ExecuteEvents.dragHandler);
            return;
        }
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (parent != null && curDirection != dragDirection)
        {
            ExecuteEvents.Execute(parent.gameObject, eventData, ExecuteEvents.endDragHandler);
            return;
        }
        base.OnEndDrag(eventData);
    }

    public override void OnScroll(PointerEventData data)
    {
        if (parent != null && curDirection != dragDirection)
        {
            ExecuteEvents.Execute(parent.gameObject, data, ExecuteEvents.scrollHandler);
            return;
        }
        base.OnScroll(data);
    }
}
