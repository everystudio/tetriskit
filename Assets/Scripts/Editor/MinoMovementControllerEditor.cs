using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace tetriskit
{
    [CustomEditor(typeof(MinoMovementController))]
    public class MinoMovementControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MinoMovementController script = target as MinoMovementController;
            base.OnInspectorGUI();

            if (GUILayout.Button("ShowGuide"))
            {
                script.ShowGuide();
            }
            if (GUILayout.Button("DeleteGuide"))
            {
                script.DeleteGuide();
            }

            if (GUILayout.Button("RotateLeft"))
            {
                script.Rotate(false);
            }
            if (GUILayout.Button("RotateRight"))
            {
                script.Rotate(true);
            }
        }
    }
}


