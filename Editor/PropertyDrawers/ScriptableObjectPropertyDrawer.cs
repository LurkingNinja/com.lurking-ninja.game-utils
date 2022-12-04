using UnityEditor;
using UnityEngine;

namespace LurkingNinja.Utils.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class ScriptableObjectPropertyDrawer : PropertyDrawer
    {
        private UnityEditor.Editor _editor;
 
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue != null)
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            EditorGUI.PropertyField(position, property, label, true);
            if (!property.isExpanded) return;

            if (!_editor)
                UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);
            if (_editor == null) return;

            EditorGUI.indentLevel++;
            _editor.DrawDefaultInspector();
            EditorGUI.indentLevel--;
        }
    }
}