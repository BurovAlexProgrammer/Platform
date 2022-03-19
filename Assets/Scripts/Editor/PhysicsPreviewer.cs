using System;
using UnityEditor;
using UnityEngine;

public class PhysicsPreviewer : EditorWindow
{
    private PreviewRenderUtility previewer;
    private GameObject target;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;


    private void OnEnable()
    {
        Debug.Log("OnEnable ");
        if (previewer == null) Initialize();
    }

    void Initialize()
    {
        previewer = new PreviewRenderUtility();
        previewer.camera.clearFlags = CameraClearFlags.Skybox;
        previewer.camera.transform.position = new Vector3(0, 0, -10);

        //previewer.lights[0] = FindObjectOfType<Light>();
        Debug.Log("Previewer initialized.");
    }

    [MenuItem("Tools/Physics Previewer")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<PhysicsPreviewer>();
        window.titleContent = new GUIContent("Physics Previewer");
    }

    private void OnSelectionChange()
    {
        Debug.Log("OnSelectionChange ");
        target = Selection.activeGameObject;
        if (target == null)
        {
            meshFilter = null;
            meshRenderer = null;
        }

        meshFilter = target.GetComponent<MeshFilter>();
        meshRenderer = target.GetComponent<MeshRenderer>();
        Repaint();
    }

    private void OnGUI()
    {
        if (target == null)
        {
            EditorGUILayout.LabelField("No GameObject selected.");
            return;
        }

        if (meshFilter == null || meshRenderer == null)
        {
            EditorGUILayout.LabelField("GameObject does not contain MeshFilter or/and MeshRenderer for displaying.");
        }
        else
        {
            Draw();
        }
    }

    private void Update()
    {
        //if (previewer == null) return;
    }

    private void Draw()
    {
        var drawRect = new Rect(0, 0, this.position.width, this.position.height);
        previewer.BeginPreview(drawRect, GUIStyle.none);
        try
        {
            previewer.DrawMesh(meshFilter.mesh, Matrix4x4.identity, meshRenderer.material, 0);
            previewer.camera.Render();
        }
        finally
        {
            var renderFrame = previewer.EndPreview();
            GUI.DrawTexture(drawRect, renderFrame);
        }
    }
}