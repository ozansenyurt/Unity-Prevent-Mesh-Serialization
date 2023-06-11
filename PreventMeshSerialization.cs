using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine.SceneManagement;

public class PreventMeshSerialization : MonoBehaviour
{

    public static PreventMeshSerialization Instance { get; private set; }
    
    public SkinnedMeshRenderer smr;

#if UNITY_EDITOR
    [System.NonSerialized] private static Mesh cachedMesh;
    
    static PreventMeshSerialization()
    {
        EditorSceneManager.sceneSaving += OnSceneSaving;
        EditorSceneManager.sceneSaved += OnSceneSaved;
    }

    private static void OnSceneSaving(Scene scene,string path)
    {
        Instance = Instance ?? FindObjectOfType<PreventMeshSerialization>();
        cachedMesh = Instance.smr.sharedMesh;
        Instance.smr.sharedMesh = null;
    }

    private static void OnSceneSaved(Scene scene)
    {
        Instance.smr.sharedMesh = cachedMesh;
        cachedMesh = null;
    }
#endif
}
