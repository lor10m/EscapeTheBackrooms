using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioPlayer : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject cubeWithImage;
    public Camera playerCamera;

    public AudioSequenceManager audioSequenceManager;
    public TextMeshProUGUI promptText;
    public bool displayOnLeftSide = true;
    public KeyCode playMusic;
    public int maxDistance;
    public AudioClip audioClip;
    private float timeLimit = 20f; // Time limit in seconds

    private bool isLookingAtCube = false;
    private bool isPlayingSequence = false;
    private float currentTime = 0f;
    private AudioSource clockTickAudioSource;
    private AudioClip clockTickAudioClip;
    private bool isFinished = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        clockTickAudioSource = GetComponent<AudioSource>(); // Get the existing AudioSource component
        clockTickAudioSource.loop = true;
        clockTickAudioSource.playOnAwake = false;
    }

    private void Start()
    {
        promptText.text = "["+ playMusic +"] Press";
        promptText.gameObject.SetActive(false); // Disable the prompt text initially
    }

    private void Update()
    {
        if (isPlayingSequence && !isFinished)
        {
            // Update the timer
            currentTime += Time.deltaTime;

            if (currentTime >= timeLimit)
            {
                // Time is up, reset the sequence
                UnityEngine.Debug.Log("Time out!");

                TimeOut();
                return;
            }
        }

        // Cast a ray from the player's camera position and direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance)) // Replace maxDistance with your desired maximum distance
        {
            if (hit.collider.gameObject == cubeWithImage)
            {
                isLookingAtCube = true;

                // Check for key press to activate sound effect
                if (Input.GetKeyDown(playMusic))
                {
                    if (isPlayingSequence)
                    {
                        UnityEngine.Debug.Log("still running sequence");
                        PlayAudio();
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Starting sequence");
                        StartSequence();
                    }
                }
            }
            else
            {
                isLookingAtCube = false;
            }
        }
        else
        {
            isLookingAtCube = false;
        }

        // Enable/disable the prompt text based on raycast result and distance
        promptText.gameObject.SetActive(isLookingAtCube && Vector3.Distance(playerCamera.transform.position, hit.point) <= maxDistance && playerCamera.gameObject.activeInHierarchy);
    }


    private void StartSequence()
    {
        UnityEngine.Debug.Log("start");
        isPlayingSequence = true;
        PlayAudio();

    }

    private void PlayAudio()
    {
        if (audioClip != null)
        {
            UnityEngine.Debug.Log("Audio Played");

            // Call AddToCurrentSequence in the AudioSequenceManager
            AudioSequenceManager sequenceManager = FindObjectOfType<AudioSequenceManager>();
            if (sequenceManager != null)
            {
                if (sequenceManager.AddToCurrentSequence(audioClip) & isPlayingSequence)
                {
                    audioManager.PlaySFX(audioClip);
                    if(sequenceManager.Size() == 1)
                    {
                        StartClockTickSound();
                    }
                    else if (sequenceManager.Size() >= 2)
                    {
                        StopTimer();
                    }

                    

                }
                else
                {
                    ResetSequence();
                    
                }
            }
        }
    }

    private void ResetSequence()
    {
        currentTime = 0f;
        isPlayingSequence = false;
        UnityEngine.Debug.Log("Sequence reset!");
    }

   

    private void TimeOut()
    {
        UnityEngine.Debug.Log("Time out!");
        StopClockTickSound(clockTickAudioSource);
        audioSequenceManager.WrongSequence();
        ResetSequence();
    }

    private void StartClockTickSound()
    {
        UnityEngine.Debug.Log("start ticking!");
        clockTickAudioSource.Play();
    }


    public static void StopClockTickSound(AudioSource clockTickAudioSource)
    {
        UnityEngine.Debug.Log("stop ticking!");
        clockTickAudioSource.Stop();
    }
    public void StopTimer()
    {
        UnityEngine.Debug.Log("stopped timer");
        isFinished = true;
    }

}
