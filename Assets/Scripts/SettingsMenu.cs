using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button musicButton;
    public Sprite musicNoteSprite;
    public Sprite muteSprite;

    private bool isMuted = false;

    private void Start()
    {
        // Add a listener to the button's onClick event
        musicButton.onClick.AddListener(ToggleMusic);
    }

    private void ToggleMusic()
    {
        isMuted = !isMuted;

        // Change the button's sprite based on the mute status
        Image buttonImage = musicButton.image;
        if (isMuted)
        {
            buttonImage.sprite = muteSprite;
        }
        else
        {
            buttonImage.sprite = musicNoteSprite;
        }

        // Implement the logic to mute/unmute the music
        // ...
    }
}
