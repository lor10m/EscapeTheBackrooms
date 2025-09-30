using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerToCustomize : MonoBehaviour
{
    private ChangeMaterial[] scripts;
    private ChangePlayerPreview playerPreview;
    private int activePlayer;

    // Start is called before the first frame update
    void Start()
    {
        scripts = FindObjectsOfType<ChangeMaterial>();
        playerPreview = FindObjectOfType<ChangePlayerPreview>();
    }

    public void ChangePlayer()
    {
        foreach (var script in scripts)
        {
            if(script.playerToCustomize == 1)
            {
                activePlayer = 2;
                script.playerToCustomize = 2;
            }
            else
            {
                activePlayer = 1;
                script.playerToCustomize = 1;
            }
        }

        UnityEngine.Debug.Log(activePlayer.ToString());

        playerPreview.ChangePreview(activePlayer);

    }
}
