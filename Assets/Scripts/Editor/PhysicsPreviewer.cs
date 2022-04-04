using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Callbacks;

public class PhysicsPreviewer : EditorWindow
{
    private GameObject target;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Camera camera;
    private bool _isSimulationRun;
    private Scene previewScene;
    private PhysicsScene previewerPhysics;
    private Vector3 worldOffset = new Vector3(0, 0, 10);
    private bool isNeedCreateScene = false;
    private bool isRenderEnabled = false;

    public bool IsSimulationRun
    {
        get => _isSimulationRun;
        private set
        {
            _isSimulationRun = value;
            OnSimulationModeChanged();
        }
    }

    [MenuItem("Tools/Physics Previewer")]
    [UsedImplicitly]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<PhysicsPreviewer>();
        window.titleContent = new GUIContent("Physics Previewer");
    }

    private void OnSimulationModeChanged()
    {
        Physics.autoSimulation = !IsSimulationRun;
    }

    void OnEnable() //OnEnabled
    {
        Debug.Log("OnEnable ");
        if (!previewScene.IsValid()) isNeedCreateScene = true;
    }

    void OnDisable() //OnDisabled
    {
        Debug.Log("OnDisable");
        EditorSceneManager.CloseScene(previewScene, true);
    }

    void Initialize()
    {
        isNeedCreateScene = false;
        Debug.Log("Initialize");
        previewScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        previewScene.name = "PhysicsPreviewer";
        previewerPhysics = previewScene.GetPhysicsScene();

        var cameraGO = new GameObject();
        cameraGO.name = "PreviewCamera";
        cameraGO.hideFlags = HideFlags.DontSave;
        cameraGO.transform.position = new Vector3(0f, 0f, -10f);
        //cameraGO.transform.eulerAngles = new Vector3(19.0f, -5.0f, 0.0f);
        camera = cameraGO.AddComponent<Camera>();
        camera.fieldOfView = 19;
        camera.clearFlags = CameraClearFlags.SolidColor;
        //camera.hideFlags = HideFlags.HideAndDontSave;
        Debug.Log("Physics Previewer initialized.");
    }

    private void OnSelectionChange()
    {
        Debug.Log("OnSelectionChange ");
        if (camera is null | !previewScene.IsValid())
        {
            Debug.LogError("OnSelectionChange -> Initialize (Something wrong - Debug it)");
            Initialize();
        }

        IsSimulationRun = false;
        DestroyImmediate(target);
        var selectedObject = Selection.activeGameObject;
        //Nothing to render
        if (!selectedObject.IsComponentExist<MeshRenderer>() || !selectedObject.IsComponentExist<MeshFilter>())
        {
            meshFilter = null;
            meshRenderer = null;
            return;
        }

        //OK
        target = Instantiate(selectedObject);
        target.hideFlags = HideFlags.DontSave;
        SceneManager.MoveGameObjectToScene(target, previewScene);
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
            isRenderEnabled = false;
        }
        else
        {
            isRenderEnabled = true;
        }

        IsSimulationRun = EditorGUILayout.Toggle("Simulation", IsSimulationRun);
        if (IsSimulationRun)
        {
            Debug.Log("sim");
        }
    }

    private void Update()
    {
        if (isNeedCreateScene) Initialize();
        if (!isRenderEnabled) return;
        if (target == null) return;

        if (IsSimulationRun)
        {
            target.transform.Rotate(Vector3.up, 10f);
            if (previewerPhysics.IsValid())
                previewerPhysics.Simulate(Time.fixedDeltaTime);
        }
        Draw();
    }

    private void Draw()
    {
        var drawRect = new Rect(0, 0, this.position.width, this.position.height);
        //previewer.BeginPreview(drawRect, GUIStyle.none);
        try
        {
            //previewer.DrawMesh(meshFilter.sharedMesh, Matrix4x4.identity, meshRenderer.sharedMaterial, 0);
            camera.Render();
        }
        finally
        {
            if (camera.targetTexture != null && camera.targetTexture.isReadable)
                GUI.DrawTexture(drawRect, camera.targetTexture);
        }
    }
}