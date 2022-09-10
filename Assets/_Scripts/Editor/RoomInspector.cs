using UnityEditor;
using UnityEngine;

namespace CoopHead.Editor
{
    [CustomEditor(typeof(Room))]
    public class RoomInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.Space(15);
            if (GUILayout.Button("Auto Setup"))
            {
                var room = target as Room;
                room.InspectorSetup();
                EditorUtility.SetDirty(room);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}