using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public string MusicOrSound;

    private bool isMuted = false;
    private float previousVolume;

    private void Start()
    {
        // Attach the button click event dynamically
        Button muteButton = GetComponent<Button>();
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            previousVolume = volumeSlider.value;
            volumeSlider.value = -80f;
        }
        else
        {
            volumeSlider.value = previousVolume;
        }

        // Find the AudioMixer with the tag "Music"
        AudioMixer musicMixer = Resources.Load<AudioMixer>(MusicOrSound);

        // Mute or unmute the AudioMixer based on the isMuted flag
        if (musicMixer != null)
        {
            musicMixer.SetFloat("Volume", isMuted ? -80f : previousVolume);
        }
    }
}
