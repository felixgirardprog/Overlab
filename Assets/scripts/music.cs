using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        slider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }
}
