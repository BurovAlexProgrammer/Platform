using UnityEditor;
using UnityEngine;

namespace Bricks.Editor
{
    [CustomEditor(typeof(Brick))]
    public class BrickEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Refresh Sequence"))
            {
                ((Brick)target).RefreshSequence();
            }
        }
    }
}