using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update

    private bool wonGame = false;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (wonGame)
        {
            SceneManager.LoadSceneAsync("VictoryScreen");
        }
    }

    public void WonGame()
    {
        wonGame = true;
    }
       
}
