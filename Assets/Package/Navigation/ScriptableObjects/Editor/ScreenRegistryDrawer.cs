#if UNITY_EDITOR
using Elselam.UnityRouter.Domain;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Elselam.UnityRouter.Installers.Editor
{
    [CustomPropertyDrawer(typeof(ScreenRegistry))]
    public class ScreenRegistryDrawer : PropertyDrawer
    {
        private const int PROPERTIES_COUNT = 9;

        private const string ID_PROPERTY = "screenId";

        private const string INTERACTOR_PROPERTY = "interactor";
        private const string INTERACTOR_PROPERTY_NAME = "interactorTypeName";

        private const string PRESENTER_PROPERTY = "presenter";
        private const string PRESENTER_PROPERTY_NAME = "presenterTypeName";

        private const string CONTROLLER_PROPERTY = "view";
        private const string CONTROLLER_PROPERTY_NAME = "viewTypeName";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var height = position.height / PROPERTIES_COUNT;

            float currentSpace = 0;

            var name = property.FindPropertyRelative(ID_PROPERTY);
            var nameRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            nameRect = EditorGUI.PrefixLabel(nameRect, new GUIContent("Id"));
            name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);

            currentSpace += 1.5f;

            var interactorVariable = property.FindPropertyRelative(INTERACTOR_PROPERTY);
            var interactorRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            interactorRect = EditorGUI.PrefixLabel(interactorRect, new GUIContent(ObjectNames.NicifyVariableName(INTERACTOR_PROPERTY)));
            interactorVariable.objectReferenceValue = EditorGUI.ObjectField(interactorRect, interactorVariable.objectReferenceValue,
                typeof(MonoScript), false);

            currentSpace += 1f;

            var interactorTypeName = property.FindPropertyRelative(INTERACTOR_PROPERTY_NAME);
            interactorTypeName.stringValue = GetName(interactorVariable.objectReferenceValue);

            var interactorNameRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            interactorNameRect = EditorGUI.PrefixLabel(interactorNameRect, new GUIContent("InteractorType"));
            EditorGUI.LabelField(interactorNameRect, interactorTypeName.stringValue);

            currentSpace += 1.5f;

            var viewVariable = property.FindPropertyRelative(PRESENTER_PROPERTY);
            var viewRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            viewRect = EditorGUI.PrefixLabel(viewRect, new GUIContent(ObjectNames.NicifyVariableName(PRESENTER_PROPERTY)));
            viewVariable.objectReferenceValue = EditorGUI.ObjectField(viewRect, viewVariable.objectReferenceValue,
                typeof(BaseScreenPresenter), false);

            currentSpace += 1f;

            var viewTypeName = property.FindPropertyRelative(PRESENTER_PROPERTY_NAME);
            viewTypeName.stringValue = GetTypeName(viewVariable.objectReferenceValue);

            var viewNameRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            viewNameRect = EditorGUI.PrefixLabel(viewNameRect, new GUIContent("PresenterType"));
            EditorGUI.LabelField(viewNameRect, viewTypeName.stringValue);

            currentSpace += 1.5f;

            var controllerVariable = property.FindPropertyRelative(CONTROLLER_PROPERTY);
            var controllerRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            controllerRect = EditorGUI.PrefixLabel(controllerRect, new GUIContent(ObjectNames.NicifyVariableName(CONTROLLER_PROPERTY)));
            controllerVariable.objectReferenceValue = EditorGUI.ObjectField(controllerRect, controllerVariable.objectReferenceValue,
                typeof(MonoScript), false);

            currentSpace += 1f;

            var controllerTypeName = property.FindPropertyRelative(CONTROLLER_PROPERTY_NAME);
            controllerTypeName.stringValue = GetName(controllerVariable.objectReferenceValue);

            var controllerNameRect = new Rect(position.x, GetY(position, height, currentSpace), position.width, height);
            controllerNameRect = EditorGUI.PrefixLabel(controllerNameRect, new GUIContent("ViewType"));
            EditorGUI.LabelField(controllerNameRect, controllerTypeName.stringValue);

            EditorGUI.EndProperty();
        }

        private string GetName(Object propertyValue) => propertyValue == null ? string.Empty : propertyValue.name;
        private string GetTypeName(Object propertyValue) => propertyValue == null ? string.Empty : propertyValue.GetType().Name;
        private float GetY(Rect position, float highValue, float space) => position.y + highValue * space;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            base.GetPropertyHeight(property, label) * PROPERTIES_COUNT;
    }
}
#endif