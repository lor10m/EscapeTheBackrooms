using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerPreview : MonoBehaviour
{
    public GameObject Player1Preview;
    public GameObject Player2Preview;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangePreview(int activePlayer)
    {
        if (activePlayer == 1 && !Player1Preview.activeSelf)
        {
            Player2Preview.SetActive(false);
            Player1Preview.SetActive(true);
        }
        else if(activePlayer == 2 && !Player2Preview.activeSelf)
        {
            Player1Preview.SetActive(false);
            Player2Preview.SetActive(true);
        }
    }
}
