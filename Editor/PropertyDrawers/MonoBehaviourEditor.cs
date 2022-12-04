using UnityEditor;
using UnityEngine;

namespace LurkingNinja.Utils.Editor.PropertyDrawers
{
    [CanEditMultipleObjects,
     CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : UnityEditor.Editor {}
}