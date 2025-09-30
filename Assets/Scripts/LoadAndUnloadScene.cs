using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadAndUnloadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
    }

    public void UnLoadScene(int sceneBuildIndex)
    {
        SceneManager.UnloadSceneAsync(sceneBuildIndex);
    }
}
