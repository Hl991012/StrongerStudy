using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewEX : ScrollRect
{
    // 枚举表示滚动方向
    private enum Direction
    {
        Horizontal,
        Vertical,
    }

    // 当前 ScrollViewEX 的滚动方向
    private Direction curDirection;

    // 拖拽期间的滚动方向
    private Direction dragDirection;

    // 对父级 ScrollViewEX 的引用（如果有的话）
    private ScrollViewEX parent;
    
    protected override void Awake()
    {
        base.Awake();

        // 如果未分配父级，请在父级层次结构中查找它
        if (parent == null)
        {
            parent = transform.parent.GetComponentInParent<ScrollViewEX>();
        }

        // 根据 'horizontal' 属性确定并设置当前滚动方向
        curDirection = horizontal ? Direction.Horizontal : Direction.Vertical;
    }

    // 开始拖拽时调用
    public override void OnBeginDrag(PointerEventData eventData)
    {
        // 根据位置的变化确定拖拽方向
        dragDirection = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y)
            ? Direction.Horizontal
            : Direction.Vertical;

        // 如果存在父级且拖拽方向与当前方向不同
        if (parent != null && curDirection != dragDirection)
        {
            // 在父级上执行开始拖拽事件
            ExecuteEvents.Execute(parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
            return;
        }

        // 如果条件不符合，则继续基本的 OnBeginDrag
        base.OnBeginDrag(eventData);
    }

    // 拖拽期间调用
    public override void OnDrag(PointerEventData eventData)
    {
        // 如果存在父级且拖拽方向与当前方向不同
        if (parent != null && curDirection != dragDirection)
        {
            // 在父级上执行拖拽事件
            ExecuteEvents.Execute(parent.gameObject, eventData, ExecuteEvents.dragHandler);
            return;
        }

        // 如果条件不符合，则继续基本的 OnDrag
        base.OnDrag(eventData);
    }

    // 拖拽结束时调用
    public override void OnEndDrag(PointerEventData eventData)
    {
        // 如果存在父级且拖拽方向与当前方向不同
        if (parent != null && curDirection != dragDirection)
        {
            // 在父级上执行结束拖拽事件
            ExecuteEvents.Execute(parent.gameObject, eventData, ExecuteEvents.endDragHandler);
            return;
        }

        // 如果条件不符合，则继续基本的 OnEndDrag
        base.OnEndDrag(eventData);
    }

    // 用户滚动时调用
    public override void OnScroll(PointerEventData data)
    {
        // 如果存在父级且拖拽方向与当前方向不同
        if (parent != null && curDirection != dragDirection)
        {
            // 在父级上执行滚动事件
            ExecuteEvents.Execute(parent.gameObject, data, ExecuteEvents.scrollHandler);
            return;
        }

        // 如果条件不符合，则继续基本的 OnScroll
        base.OnScroll(data);
    }
}
