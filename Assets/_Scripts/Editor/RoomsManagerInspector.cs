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
            if (GUILayout.Button("Update rooms"))
            {
                roomsManager.UpdateRooms();
            }
            
            GUILayout.Space(10);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("checkpoints"));
            if (GUILayout.Button("Get all checkpoints"))
            {
                roomsManager.checkpoints = GameObject.FindObjectsOfType<Checkpoint>();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}