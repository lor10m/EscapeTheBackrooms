using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ButtonSequenceManager : MonoBehaviour
{
    public List<string> correctSequenceList; // The correct sequence of button identifiers (e.g., ["green", "red", "blue"])
    private List<string> playerSequence; // The player's input sequence
    AudioManager audioManager;
    public AudioClip rightAudioClip;
    public AudioClip wrongAudioClip;

    
    public OpenDoor openDoorScript;
    private bool correctSequence = false;
    private int sequenceCount = 0;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    private void Start()
    {
        playerSequence = new List<string>();
    }

    private void Update()
    {
        if (correctSequence)
        {
            UnityEngine.Debug.Log("activate door");
            openDoorScript.ActivateDoor();
            openDoorScript.LastRiddleSolved();
        }
        
    }

    public void AddToSequence(string buttonIdentifier)
    {
        if(!correctSequence)
        {
            UnityEngine.Debug.Log("Button Identifier: " + buttonIdentifier);

            playerSequence.Add(buttonIdentifier);

            // Check if the player's sequence matches the correct sequence
            bool sequencesMatch = false;

            for (int i = sequenceCount; i < playerSequence.Count; i++)
            {
                if (playerSequence[i] == correctSequenceList[i])
                {
                    sequencesMatch = true;
                    audioManager.PlaySFX(rightAudioClip);
                    
                }
                else
                {
                    sequencesMatch = false;
                    break;
                }
            }
            sequenceCount++;
            if (playerSequence.Count == correctSequenceList.Count && sequencesMatch)
            {
                audioManager.PlaySFX(rightAudioClip);
                UnityEngine.Debug.Log("Correct sequence entered!");

                // Set correctSequence to true when the sequences match
                correctSequence = true;

                // Perform the desired action when the sequences match
                // Trigger an event, play a sound, etc.
            }
            else if (!sequencesMatch)
            {
                UnityEngine.Debug.Log("Wrong sequence entered. Resetting...");
                audioManager.PlaySFX(wrongAudioClip);
                // Reset the player's input sequence if the sequences don't match
                ResetSequence();
            }
        }
        
    }





    private void ResetSequence()
    {
        playerSequence.Clear();
        UnityEngine.Debug.Log("Wrong sequence entered. Resetting...");
        // Perform any necessary actions when the sequence is reset
        sequenceCount = 0;
    }
}
