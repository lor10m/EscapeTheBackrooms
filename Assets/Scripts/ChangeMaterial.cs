using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    [SerializeField] private string tagName;

    [SerializeField] private Material[] materials;

    public int playerToCustomize;

    private Renderer objRenderer;

    private string objName;
    
    private int[] index = new int[3] { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        playerToCustomize = 1;
    }

    // Update is called once per frame
    public void ChangeObjMaterial(bool isLeftButton)
    {
        UnityEngine.Debug.Log(playerToCustomize);
        if (isLeftButton)
        {
            if (index[playerToCustomize] == 0)
            {
                index[playerToCustomize] = materials.Length - 1;
            }
            else
            {
                --index[playerToCustomize];
            }
        }
        else
        {
            if (index[playerToCustomize] == materials.Length - 1)
            {
                index[playerToCustomize] = 0;
            }
            else
            {
                ++index[playerToCustomize];
            }
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player" + playerToCustomize.ToString() + tagName);
        UnityEngine.Debug.Log("Player" + playerToCustomize.ToString() + tagName);
        foreach (GameObject obj in objects)
        {
            objRenderer = obj.GetComponent<Renderer>();
            objRenderer.material = materials[index[playerToCustomize]];
        }

    }
}
