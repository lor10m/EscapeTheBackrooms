using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public int sceneIndexToKeepLoaded; // Index of the scene you want to keep loaded
    private int escapeScreenIndex = 2;
    // Update is called once per frame
    void Update()
    {
        if (!SceneManager.GetSceneByBuildIndex(0).IsValid())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 0)
                {
                    UnloadAllScenesExceptDesired();

                    Cursor.visible = false;
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                    SceneManager.LoadScene(escapeScreenIndex, LoadSceneMode.Additive);
                    Cursor.visible = true;
                }
            }
        }
        
    }

    public void UnloadAllScenesExceptDesired()
    {
        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.buildIndex != sceneIndexToKeepLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

}
