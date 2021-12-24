#if UNITY_EDITOR
using Elselam.UnityRouter.Domain;
using UnityEditor;
using UnityEngine;

namespace Elselam.UnityRouter.Installers.Editor
{
    [CustomPropertyDrawer(typeof(ScreenRegistry))]
    public class ScreenRegistryDrawer : PropertyDrawer
    {
        private const int PropertiesCount = 9;

        private const string IdProperty = "screenId";

        private const string InteractorProperty = "interactor";
        private const string InteractorPropertyName = "interactorTypeName";

        private const string PresenterProperty = "presenter";
        private const string PresenterPropertyName = "presenterTypeName";

        private const string ViewProperty = "view";
        private const string ViewPropertyName = "viewTypeName";

        private const float DefaultSpace = 1.5f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var height = position.height / PropertiesCount;

            float currentSpace = 0;

            var name = property.FindPropertyRelative(IdProperty);
            var nameRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            nameRect = EditorGUI.PrefixLabel(nameRect, new GUIContent("Id"));
            name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);

            currentSpace += DefaultSpace;

            new PropertyNameDrawer(position, property, height, ref currentSpace,
                InteractorProperty, InteractorPropertyName, "InteractorType", typeof(MonoScript));

            currentSpace += DefaultSpace;

            new PropertyNameDrawer(position, property, height, ref currentSpace,
                PresenterProperty, PresenterPropertyName, "PresenterType", typeof(BaseScreenPresenter));

            currentSpace += DefaultSpace;

            new PropertyNameDrawer(position, property, height, ref currentSpace,
                ViewProperty, ViewPropertyName, "ViewType", typeof(MonoScript));

            EditorGUI.EndProperty();
        }

        private float GetY(Rect position, float highValue, float space) => position.y + highValue * space;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => base.GetPropertyHeight(property, label) * PropertiesCount;
    }
}
#endif