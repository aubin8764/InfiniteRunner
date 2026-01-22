using UnityEngine;
using Component.SceneLoader;

public class UIMainMenuController : MonoBehaviour
{
    private void Start()
    {
        SaveService.Save();
    }
    public void PlayGame()
    {
        // Load Scenes
        SceneLoaderService.LoadLevel();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
