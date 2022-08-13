// using UnityEditor;
// using UnityEngine;
//
// namespace CoopHead.Editor
// {
//     [CustomPropertyDrawer(typeof(ObjectToggler.List2D<>))]
//     public class List2DPropertyDrawer : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property,
//             GUIContent label)
//         {
//             // base.OnGUI(position, property, new GUIContent(""));
//             EditorGUI.PropertyField(position, property.FindPropertyRelative("arrays"), label);
//         }
//
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             return base.GetPropertyHeight(property, label);
//         }
//     }
// }