// using UnityEditor;
// using UnityEngine;
//
// namespace CoopHead.Editor
// {
//     [CustomPropertyDrawer(typeof(ObjectToggler.List<>))]
//     public class ListPropertyDrawer : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property,
//             GUIContent label)
//         {
//             // base.OnGUI(position, property, new GUIContent(""));
//             EditorGUI.PropertyField(position, property.FindPropertyRelative("values"), label);
//         }
//
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             var prop = property.FindPropertyRelative("values").arraySize;
//             return base.GetPropertyHeight(property, label) * prop;
//         }
//     }
// }