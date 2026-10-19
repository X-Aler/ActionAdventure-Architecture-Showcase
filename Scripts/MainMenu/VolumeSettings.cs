using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (masterSlider) masterSlider.onValueChanged.AddListener(SetMaster);
        if (musicSlider) musicSlider.onValueChanged.AddListener(SetMusic);
        if (sfxSlider) sfxSlider.onValueChanged.AddListener(SetSFX);
    }

    public void SetMaster(float v)
    {
        mixer.SetFloat("Master", Mathf.Log10(v) * 20f);
    }

    public void SetMusic(float v)
    {
        mixer.SetFloat("Music", Mathf.Log10(v) * 20f);
    }

    public void SetSFX(float v)
    {
        mixer.SetFloat("SFX", Mathf.Log10(v) * 20f);
    }
}