using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _as;

    private AudioClip _clip;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _clip = clip;

        _as.PlayOneShot(_clip);

        _clip = null;
    }
}
