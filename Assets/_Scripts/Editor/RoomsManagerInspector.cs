using UnityEditor;
using UnityEngine;

namespace CoopHead.Editor
{
    [CustomEditor(typeof(RoomsManager))]
    public class RoomsManagerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var roomsManager = target as RoomsManager;
            if (!roomsManager)
                return;

            serializedObject.Update();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rooms"));
            GUILayout.Space(2);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("checkpoints"));
            
            GUILayout.Space(10);
            if (GUILayout.Button("Auto setup"))
            {
                roomsManager.InspectorSetup();
                EditorUtility.SetDirty(roomsManager);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}