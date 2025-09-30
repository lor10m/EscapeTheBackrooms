using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Find the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonSound(AudioClip buttonSound)
    {
        // Play the button sound
        audioSource.PlayOneShot(buttonSound);
    }
}
