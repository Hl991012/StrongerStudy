using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

[InitializeOnLoad]
public class AutoCreateUIComponent : Editor
{
    // 静态变量，用于存储上一个生成的对象、对象的名称和根对象
    static string lastName;
    static Transform lastRoot;
    static GameObject lastGenObj;
    static Transform newParent;

    static AutoCreateUIComponent()
    {
        // 注册场景GUI事件
        SceneView.duringSceneGui += OnSceneGUI;
        
        // 鼠标款选功能
        SceneView.duringSceneGui += OnSceneGUI1;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        // 如果上一个生成的对象不为空，则检查并删除之前生成的 SpriteRenderer
        if (lastName != null && lastRoot != null && lastGenObj != null)
        {
            var index = lastGenObj.transform.GetSiblingIndex() + 1;
            if (index >= lastGenObj.transform.parent.childCount)
            {
                // 重置静态变量
                lastName = null;
                lastRoot = null;
                lastGenObj = null;
                newParent = null;
                return;
            }
            var sr = lastGenObj.transform.parent.GetChild(index);
            
            // 如果找到下一个兄弟节点并且有 SpriteRenderer，则销毁它
            if (sr != null && sr.GetComponent<SpriteRenderer>() != null)
            {
                DestroyImmediate(sr.gameObject);
                lastGenObj.transform.SetParent(newParent);
                Selection.activeGameObject = lastGenObj;
            }

            // 重置静态变量
            lastName = null;
            lastRoot = null;
            lastGenObj = null;
            newParent = null;
        }

        // 获取当前事件
        Event e = Event.current;
        if (e.type != EventType.DragPerform)
        {
            return;
        }

        // 如果拖拽的对象数量大于1，则返回
        if (DragAndDrop.objectReferences.Length > 1)
        {
            return;
        }

        if (!IsPrefabMode(out var root))
        {
            return;
        }

        if (root.GetComponent<Canvas>() == null)
        {
            return;
        }

        // 获取拖拽的对象
        var obj = DragAndDrop.objectReferences[0];

        // 如果拖拽的对象是 Texture2D
        if (obj is Texture2D || obj is Sprite)
        {
            newParent = TryGetParentAtMouse(root.GetComponent<RectTransform>());

            if (e.alt)
            {
                // 创建 Button，并将其设置为上一个生成的对象
                var button = CreateButtonComponent(GetSprite(obj));
                button.transform.SetParent(root);
                PlaceUIObjectAtMouse(button.transform);
                lastGenObj = button.gameObject;
            }
            else
            {
                // 创建 Image，并将其设置为上一个生成的对象
                var image = CreateImageComponent(GetSprite(obj));
                image.transform.SetParent(root);
                PlaceUIObjectAtMouse(image.transform);
                lastGenObj = image.gameObject;
            }

            // 存储上一个生成的对象的名称和根对象
            lastName = obj.name;
            lastRoot = root;
        }
    }

    private static Sprite GetSprite(Object obj)
    {
        if (obj is Sprite)
        {
            return obj as Sprite;
        }
        else
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(obj));
        }
    }

    private static Image CreateImageComponent(Sprite sprite)
    {
        // 创建 Image 组件
        var image = new GameObject("NewImage").AddComponent<Image>();
        image.sprite = sprite;
        image.SetNativeSize();
        Decorate(image);
        return image;
    }

    private static Button CreateButtonComponent(Sprite sprite)
    {
        // 创建 Button 组件
        var button = new GameObject("NewButton").AddComponent<Image>().gameObject.AddComponent<Button>();
        button.GetComponent<Image>().sprite = sprite;
        button.GetComponent<Image>().SetNativeSize();
        Decorate(button);
        return button;
    }

    private static void Decorate<T>(T component)
    {
        switch (component)
        {
            case Image image:
                image.raycastTarget = false;
                break;
            case Button button:
                break;
            default:
                break;
        }
    }

    private static bool IsPrefabMode(out Transform root)
    {
        // 检查是否在 Prefab 模式下，并返回 Prefab 的根对象
        root = null;
        var stage = PrefabStageUtility.GetCurrentPrefabStage();
        if (stage == null)
        {
            return false;
        }
        root = stage.prefabContentsRoot.transform;
        return true;
    }

    private static void PlaceUIObjectAtMouse(Transform uiObjectTransform)
    {
        uiObjectTransform.position = GetWorldPosAtMouse();
    }

    private static Transform TryGetParentAtMouse(RectTransform rectTransform)
    {
        var pos = GetWorldPosAtMouse();

        if (GetRectTransformAtMouse(rectTransform, pos) is { } r)
        {
            return r;
        }

        return rectTransform;
    }

    // 反向遍历rectTransform的子物体, 找到第一个包含鼠标位置的子物体, 递归调用
    private static RectTransform GetRectTransformAtMouse(RectTransform rectTransform, Vector3 mousePos)
    {
        if (rectTransform.childCount < 1)
        {
            var point1 = rectTransform.InverseTransformPoint(mousePos);
            return rectTransform.rect.Contains(point1) ? rectTransform : null;
        }

        for (int i = rectTransform.childCount - 1; i >= 0; i--)
        {
            if (GetRectTransformAtMouse(rectTransform.GetChild(i) as RectTransform, mousePos) is { } r)
            {
                return r;
            }
        }

        var point = rectTransform.InverseTransformPoint(mousePos);
        return rectTransform.rect.Contains(point) ? rectTransform : null;
    }

    private static Vector3 GetWorldPosAtMouse()
    {
        // 将鼠标位置转换为场景中的射线
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        // 获取射线与平面的交点
        Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            // 设置UI对象的位置为交点位置
            return ray.GetPoint(rayDistance);
        }
        return Vector3.zero;
    }

    #region 框选功能

    private static bool isSelecting;
    private static Vector2 startDragPosition;
    private static Vector2 endDragPosition;
    
    private static void OnSceneGUI1(SceneView sceneView)
    {
        Event currentEvent = Event.current;
        //HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        if (true)
        {
            switch (currentEvent.type)
            {
                case EventType.MouseDown:
                    if (currentEvent.button == 0)
                    {
                        isSelecting = true;
                        startDragPosition = currentEvent.mousePosition;
                        endDragPosition = currentEvent.mousePosition;
                    }
                    break;

                case EventType.MouseUp:
                    if (currentEvent.button == 0 && currentEvent.shift)
                    {
                        isSelecting = false;
                        HandleSelection();
                    }
                    break;

                case EventType.MouseDrag:
                    if (isSelecting)
                    {
                        endDragPosition = currentEvent.mousePosition;
                        HandleSelectionBox();
                    }
                    break;
            }
        }
    }

    private static void HandleSelectionBox()
    {
        Rect selectionRect = new Rect(
            Mathf.Min(startDragPosition.x, endDragPosition.x),
            Mathf.Min(startDragPosition.y, endDragPosition.y),
            Mathf.Abs(endDragPosition.x - startDragPosition.x),
            Mathf.Abs(endDragPosition.y - startDragPosition.y)
        );

        EditorGUI.DrawRect(selectionRect, new Color(0.5f, 0.5f, 0.5f, 0.5f));

        Handles.BeginGUI();
        Handles.DrawSolidRectangleWithOutline(
            new Rect(selectionRect.position, new Vector2(0, selectionRect.height)),
            new Color(0.5f, 0.5f, 0.5f, 0.5f),
            Color.green
        );
        Handles.DrawSolidRectangleWithOutline(
            new Rect(selectionRect.position, new Vector2(selectionRect.width, 0)),
            new Color(0.5f, 0.5f, 0.5f, 0.5f),
            Color.green
        );
        Handles.DrawSolidRectangleWithOutline(
            new Rect(selectionRect.position + new Vector2(0, selectionRect.height), new Vector2(selectionRect.width, 0)),
            new Color(0.5f, 0.5f, 0.5f, 0.5f),
            Color.green
        );
        Handles.DrawSolidRectangleWithOutline(
            new Rect(selectionRect.position + new Vector2(selectionRect.width, 0), new Vector2(0, selectionRect.height)),
            new Color(0.5f, 0.5f, 0.5f, 0.5f),
            Color.green
        );
        Handles.EndGUI();
    }

    private static void HandleSelection()
    {
        // Implement your custom selection logic here based on the selected objects within the selectionRect.
        // For example, you can use HandleUtility.PickRectObjects to get the selected objects.

        // Clear the selectionRect after handling selection.
        startDragPosition = Vector2.zero;
        endDragPosition = Vector2.zero;
        Debug.LogError(1111);
    }

    #endregion
}
