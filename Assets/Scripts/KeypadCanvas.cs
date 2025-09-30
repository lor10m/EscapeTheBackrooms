using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadCanvas : MonoBehaviour
{

    [SerializeField] private string PIN;
    [SerializeField] private GameObject keypad;
    [SerializeField] private OpenDoor door;
    [SerializeField] AudioClip wrongInputSound;
    [SerializeField] AudioClip correctInputSound;
    [SerializeField] AudioClip clickButtonSound;
    [SerializeField] AudioManager audioManager;

    private string currentInput;

    // Start is called before the first frame update
    void Start()
    {
        currentInput = "";
    }

    public void InputNumber(string number)
    {
        audioManager.PlaySFX(clickButtonSound);
        if (currentInput.Length < PIN.Length)
        {
            currentInput += number;
        }
    }

    public void ClearInput()
    {
        currentInput = "";
    }

    public void ValidateInput()
    {
        if(currentInput == PIN)
        {
            audioManager.PlaySFX(correctInputSound);
            UnityEngine.Debug.Log("correct");
            door.ActivateDoor();
            keypad.SetActive(false);
        }
        else
        {
            audioManager.PlaySFX(wrongInputSound);
            ClearInput();
        }
    }

}
