
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    // Start is called before the first frame update

    public void setMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", volume);
    }
    public void setSFXVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("SFX", volume);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
