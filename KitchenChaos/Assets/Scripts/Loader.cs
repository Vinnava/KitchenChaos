using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    
    private static Scene targetScene;
    
    public static void Load(Scene targetSceneLoc) {
        targetScene = targetSceneLoc;

        SceneManager.LoadScene(nameof(Scene.LoadingScene));
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
