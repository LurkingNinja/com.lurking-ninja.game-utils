using LurkingNinja.Utils.Types;
using UnityEditor;
using UnityEngine;

namespace LurkingNinja.Utils.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            return valueProperty != null ? EditorGUI.GetPropertyHeight(valueProperty) : 12f;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            var hasValueProperty = property.FindPropertyRelative("hasValue");

            position.width -= 24;
            EditorGUI.BeginDisabledGroup(!hasValueProperty.boolValue);
            EditorGUI.PropertyField(position, valueProperty, label, true);
            EditorGUI.EndDisabledGroup();

            position.x += position.width + 24;
            position.width = position.height = EditorGUI.GetPropertyHeight(hasValueProperty);
            position.x -= position.width + EditorGUI.indentLevel * position.height;
            EditorGUI.PropertyField(position, hasValueProperty, GUIContent.none);
        }
    }
}
