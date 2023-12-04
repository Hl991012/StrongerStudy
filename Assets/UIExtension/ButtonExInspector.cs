using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(ButtonExtension), true)]
    [CanEditMultipleObjects]
    public class ButtonExInspector : SelectableEditor
    {
        private ButtonExtension buttonExtension;

        SerializedProperty m_OnDoubleClickProperty;
        SerializedProperty m_OnClickProperty;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            m_OnDoubleClickProperty = serializedObject.FindProperty("doubleClickEvent");
            m_OnClickProperty = serializedObject.FindProperty("m_OnClick");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            buttonExtension = (ButtonExtension)target;
            EditorGUILayout.Space();

            buttonExtension.singleClickEnabled = EditorGUILayout.Toggle("启用单击", buttonExtension.singleClickEnabled);
            if (buttonExtension.singleClickEnabled)
            {
                buttonExtension.doubleClickEnabled = false;
                EditorGUILayout.PropertyField(m_OnClickProperty);  
            }
            
            buttonExtension.doubleClickEnabled = EditorGUILayout.Toggle("启用双击", buttonExtension.doubleClickEnabled);
            if (buttonExtension.doubleClickEnabled)
            {
                buttonExtension.doubleClickTime = EditorGUILayout.FloatField("双击间隔", buttonExtension.doubleClickTime);
                buttonExtension.singleClickEnabled = false;
                EditorGUILayout.PropertyField(m_OnDoubleClickProperty);
            }
            
            serializedObject.ApplyModifiedProperties();
            if(GUI.changed)
                EditorUtility.SetDirty(target);
        }
    }   
}

