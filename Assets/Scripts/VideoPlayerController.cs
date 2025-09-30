using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoClip initialVideoClip;
    public VideoClip newVideoClip;
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Set the initial video clip and start playing
        videoPlayer.clip = initialVideoClip;
        videoPlayer.Play();
    }

    // Call this method to switch to a new video clip
    public void SwitchVideo()
    {
        // Stop the current video playback
        videoPlayer.Stop();

        // Assign the new video clip
        videoPlayer.clip = newVideoClip;

        // Restart the video playback
        videoPlayer.Play();
    }
}
