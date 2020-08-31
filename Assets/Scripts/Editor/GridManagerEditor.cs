using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace tetriskit
{
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GridManager script = target as GridManager;
            base.OnInspectorGUI();

            if (GUILayout.Button("Setup"))
            {
                script.Setup();
            }
        }
    }
}



