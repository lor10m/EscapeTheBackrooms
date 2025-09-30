using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BacktoMainMenu()
    {
        // Unload all currently loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }

        // Load the scene with the specified index
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
