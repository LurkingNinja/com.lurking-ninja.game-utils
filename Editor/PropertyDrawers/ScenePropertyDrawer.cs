using System;
using System.Linq;
using LurkingNinja.Utils.Attributes;
using UnityEditor;
using UnityEngine;

namespace LurkingNinja.Utils.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SceneDrawerAttribute))]
    public class SceneDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SceneDrawer] with strings.");
                return;
            }
            var sceneObject = GetSceneObject(property.stringValue);
            var scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);

            if (scene == null)
            {
                property.stringValue = "";
                return;
            }

            if (scene.name == property.stringValue) return;
            var sceneObj = GetSceneObject(scene.name);
            if (sceneObj is null) return;
            property.stringValue = scene.name;
        }

        private static SceneAsset GetSceneObject(string sceneObjectName) => string.IsNullOrEmpty(sceneObjectName) 
            ? null 
            : (from editorScene 
                    in EditorBuildSettings.scenes
                where editorScene.path.IndexOf(sceneObjectName, StringComparison.Ordinal) != -1
                select AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset)
            .FirstOrDefault();
    }
}