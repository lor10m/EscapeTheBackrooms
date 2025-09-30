using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSequenceManager : MonoBehaviour
{
    AudioManager audioManager;
    public List<AudioClip> correctSequence;
    private List<AudioClip> currentSequence;
    public OpenDoor openDoorScript;
    public AudioClip correctAudioClip;
    public AudioSource timeTickingaudioSource;
    public AudioPlayer audioPlayer;

    private bool finished;
    

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentSequence = new List<AudioClip>();
    }

    public bool AddToCurrentSequence(AudioClip audioClip)
    {
        currentSequence.Add(audioClip);
        UnityEngine.Debug.Log("Added audio to current sequence: " + audioClip.name);

        // Check if the current clip is incorrect
        if (currentSequence[currentSequence.Count - 1] != correctSequence[currentSequence.Count - 1])
        {

            UnityEngine.Debug.Log("XX");
            WrongSequence();
            return false;
        }
        else
        {
            CheckSequence();
            return true;
        }
    }
    public bool isFinsihed()
    {
        return finished;
    }

    private void CheckSequence()
    {
        if (currentSequence.Count == correctSequence.Count)
        {
            bool isCorrect = true;

            for (int i = 0; i < currentSequence.Count; i++)
            {
                if (currentSequence[i] != correctSequence[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                UnityEngine.Debug.Log("X");
                SequenceCompleted();
            }
            else
            {

                UnityEngine.Debug.Log("Y");
                WrongSequence();
            }
        }
    }

    public void WrongSequence()
    {
        currentSequence.Clear();
        audioManager.PlayWrongSFX();
        UnityEngine.Debug.Log("IIIIncorrect sequence. Resetting...");

        AudioPlayer.StopClockTickSound(timeTickingaudioSource);
    }

    private void SequenceCompleted()
    {
        finished = true;
        openDoorScript.ActivateDoor();
        UnityEngine.Debug.Log("Sequence completed!");
        AudioPlayer.StopClockTickSound(timeTickingaudioSource);
        StartCoroutine(PlayCorrectAudioWithDelay());

        // Stop the timer
        audioPlayer.StopTimer();
    }
    private IEnumerator PlayCorrectAudioWithDelay()
    {
        yield return new WaitForSeconds(1.5f); // Delay for 3 seconds

        audioManager.PlaySFX(correctAudioClip);
    }

    private void ResetSequence()
    {
        currentSequence.Clear();
        UnityEngine.Debug.Log("Resetting...");
    }
    public int Size()
    {
        return currentSequence.Count;
    }
}
