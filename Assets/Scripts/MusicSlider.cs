using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;

    [SerializeField] Slider soundSlider;

    [SerializeField] string VolumeName = "MasterVolume";

    private string savedVolume;

    private void Start()
    {
        savedVolume = "Saved" + VolumeName;

        SetVolume(PlayerPrefs.GetFloat(savedVolume));
    }

    public void SetVolume(float _value)
    {
        if(_value < 1)
        {
            _value = .001f;
        }

        RefreshSlider(_value); //fsddddddd

        PlayerPrefs.SetFloat(savedVolume, _value);
        masterMixer.SetFloat(VolumeName, Mathf.Log10(_value / 100) * 20f);
    }

    
    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);

    }

    public void RefreshSlider(float _value)
    {
        if(soundSlider != null)
        {
            soundSlider.value = _value; //fsddddddd
        }

    }
}
