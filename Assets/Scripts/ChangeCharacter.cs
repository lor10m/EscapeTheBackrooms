using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private GameObject currentCharacter;
    [SerializeField] private GameObject characters;
    [SerializeField] private Vector3 spawnPosition;

    void Start()
    {

    }

    public void Change()
    {
        currentCharacter.SetActive(false);
        characters.SetActive(true);
        currentCharacter = characters;
    }
}
