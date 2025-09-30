using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;

    public string toggleButton;
        
    public bool cams1active = true;
    public bool cams2active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(toggleButton))
        {
            ToggleCams();
        }
    }



    void ToggleCams()  {
        if (cams1active)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);

            cams1active = false;
        }
        else
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);
            
            cams1active = true;
        }
       
    }
   
    
}
