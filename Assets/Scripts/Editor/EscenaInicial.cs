using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EscenaInicial
{
    static EscenaInicial()
    {
        var escenas = EditorBuildSettings.scenes;
        if (escenas.Length > 0)
            EditorSceneManager.playModeStartScene =
                AssetDatabase.LoadAssetAtPath<SceneAsset>(escenas[0].path);
    }
}