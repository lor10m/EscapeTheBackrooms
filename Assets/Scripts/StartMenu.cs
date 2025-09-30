using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private const string LastSceneKey = "LastScene";
    private const int StartMenuSceneIndex = 0;
    private const int GameSceneIndex = 1;
    void Start()
    {
        if (!SceneManager.GetSceneByBuildIndex(GameSceneIndex).IsValid())
        {
            SceneManager.LoadSceneAsync(GameSceneIndex, LoadSceneMode.Additive);
            Cursor.visible = true;
        }
    }
    public void PlayGame()
    {
        Cursor.visible = false;
        SceneManager.UnloadSceneAsync(StartMenuSceneIndex);
    }

    public void Options()
    {
        SaveLastScene();
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    public void Back()
    {
        SceneManager.UnloadSceneAsync(3);
    }

    public void Character()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }

    private void SaveLastScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt(LastSceneKey, currentScene.buildIndex);
    }
}
