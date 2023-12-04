// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class DragAndDropSpriteInHierarchy : Editor
// {
//     private static Texture2D draggedTexture;
//     private static Transform selectedTrans;
//
//     [InitializeOnLoadMethod]
//     private static void Initialize()
//     {
//         //EditorApplication.win += HierarchyWindowItemOnGUI1;
//         //EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI2;
//         SceneView.duringSceneGui += HierarchyWindowItemOnGUI1;
//     }
//
//     private static GameObject genObj = null;
//
//     private static Event currentEvent;
//
//     private static string curPath;
//     private static void HierarchyWindowItemOnGUI1(SceneView sceneView)
//     {
//         currentEvent = Event.current;
//         if (currentEvent.type == EventType.DragUpdated || currentEvent.type == EventType.DragPerform)
//         {
//             DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
//
//             if (currentEvent.type == EventType.DragPerform)
//             {
//                 DragAndDrop.AcceptDrag();
//
//                 foreach (Object draggedObject in DragAndDrop.objectReferences)
//                 {
//                     if (draggedObject is Texture2D)
//                     {
//                         draggedTexture = (Texture2D)draggedObject;
//                         CreateSpriteRendererOnGameObject(draggedObject.GetInstanceID(), sceneView);
//                     }
//
//                     if (draggedObject is Sprite)
//                     {
//                         genObj = new GameObject(draggedObject.name);
//                         curPath = AssetDatabase.GetAssetPath(Selection.activeObject);
//                         genObj.AddComponent<Image>().sprite = draggedObject as Sprite;
//                         genObj.GetComponent<Image>().SetNativeSize();
//                         // if (selectedTrans != null)
//                         // {
//                         //     genObj.transform.SetParent(selectedTrans);
//                         // }
//                         genObj.transform.position = currentEvent.mousePosition;
//                         currentEvent.Use();
//                     }
//                 }
//             }
//         }
//     }
//
//     private static void HierarchyWindowItemOnGUI2(int instanceID, Rect selectionRect)
//     {
//         currentEvent = Event.current;
//         if (currentEvent.type == EventType.DragExited)
//         {
//             selectedTrans = (EditorUtility.InstanceIDToObject(instanceID) as GameObject)?.transform;
//             if (genObj != null && selectedTrans != null)
//             {
//                 genObj.transform.SetParent(selectedTrans);
//                 // ExpandObjectAndChildren(genObj.transform.parent.gameObject);
//                 // EditorApplication.RepaintHierarchyWindow();
//             }
//         }
//     }
//     
//     private static void CreateSpriteRendererOnGameObject(int instanceID, SceneView sceneView)
//     {
//         Object selectedObject = EditorUtility.InstanceIDToObject(instanceID);
//
//         Debug.LogError(selectedObject);
//         if (selectedObject != null && selectedObject is Texture2D)
//         {
//             genObj = new GameObject(selectedObject.name);
//             curPath = AssetDatabase.GetAssetPath(Selection.activeObject);
//             genObj.AddComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(curPath);
//             genObj.GetComponent<Image>().SetNativeSize();
//             genObj.transform.position = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition).origin;
//             currentEvent.Use();
//         }
//     }
//     
//     private static void ExpandObjectAndChildren(GameObject obj)
//     {
//         if (obj != null)
//         {
//             SerializedObject serializedObject = new SerializedObject(obj);
//             SerializedProperty children = serializedObject.FindProperty("m_Children");
//
//             // for (int i = 0; i < children.arraySize; i++)
//             // {
//             //     SerializedProperty child = children.GetArrayElementAtIndex(i);
//             //     ExpandObjectAndChildren(child.objectReferenceValue as GameObject);
//             // }
//
//             obj.hideFlags &= ~HideFlags.HideInHierarchy; // 显示对象
//             //obj.SetActive(true); // 激活对象
//         }
//     }
// }