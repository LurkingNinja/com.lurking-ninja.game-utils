using LurkingNinja.Utils.Attributes;
using LurkingNinja.Utils.Types;
using UnityEditor;
using UnityEngine;

namespace LurkingNinja.Utils.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(IntRange), true)]
    public class IntRangePropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            var minProp = property.FindPropertyRelative("minValue");
            var maxProp = property.FindPropertyRelative("maxValue");

            var minValue = minProp.intValue;
            var maxValue = maxProp.intValue;

            var rangeMin = 0f;
            var rangeMax = 1f;

            var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
            if (ranges.Length > 0)
            {
                rangeMin = Mathf.RoundToInt(ranges[0].Min);
                rangeMax = Mathf.RoundToInt(ranges[0].Max);
            }

            const float rangeBoundsLabelWidth = 40f;

            var rangeBoundsLabel1Rect = new Rect(position) {width = rangeBoundsLabelWidth};
            GUI.Label(rangeBoundsLabel1Rect, new GUIContent(minValue.ToString("F2")));
            position.xMin += rangeBoundsLabelWidth;

            var rangeBoundsLabel2Rect = new Rect(position);
            rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
            GUI.Label(rangeBoundsLabel2Rect, new GUIContent(maxValue.ToString("F2")));
            position.xMax -= rangeBoundsLabelWidth;

            EditorGUI.BeginChangeCheck();
            float minValueF = minValue;
            float maxValueF = maxValue;
            EditorGUI.MinMaxSlider(position, ref minValueF, ref maxValueF, rangeMin, rangeMax);
            if (EditorGUI.EndChangeCheck())
            {
                minProp.intValue = Mathf.RoundToInt(minValueF);
                maxProp.intValue = Mathf.RoundToInt(maxValueF);
            }

            EditorGUI.EndProperty();
        }
    }
}