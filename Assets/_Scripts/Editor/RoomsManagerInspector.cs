using UnityEditor;
using UnityEngine;

namespace CoopHead.Editor
{
    [CustomEditor(typeof(RoomsManager))]
    public class RoomsManagerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var roomsManager = target as RoomsManager;
            if (!roomsManager)
                return;
            
            GUILayout.Space(20);
            if (GUILayout.Button("Update rooms"))
            {
                roomsManager.UpdateRooms();
            }
        }
    }
}