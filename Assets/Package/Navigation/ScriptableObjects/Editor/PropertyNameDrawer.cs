#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Elselam.UnityRouter.Installers.Editor
{
    public class PropertyNameDrawer : PropertyDrawer
    {
        public PropertyNameDrawer(Rect position, SerializedProperty serializedProperty, float height, 
            ref float currentSpace, string property, string propertyName, string displayName, Type type)
        {
            var relativeProperty = serializedProperty.FindPropertyRelative(property);
            var fieldRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            fieldRect = EditorGUI.PrefixLabel(fieldRect, new GUIContent(ObjectNames.NicifyVariableName(property)));
            relativeProperty.objectReferenceValue = EditorGUI.ObjectField(fieldRect, relativeProperty.objectReferenceValue, type, false);

            currentSpace += 1f;

            var interactorTypeName = serializedProperty.FindPropertyRelative(propertyName);
            interactorTypeName.stringValue = GetName(relativeProperty.objectReferenceValue);

            var nameRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            nameRect = EditorGUI.PrefixLabel(nameRect, new GUIContent(displayName));
            EditorGUI.LabelField(nameRect, interactorTypeName.stringValue);
        }

        private string GetName(Object propertyValue) => propertyValue == null ? string.Empty : propertyValue.name;
        private float GetY(Rect position, float highValue, float space) => position.y + highValue * space;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => base.GetPropertyHeight(property, label) * 2;
    }
}
#endif