using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioLoaded : MonoBehaviour
{
    AudioSource _as;

    [SerializeField] AudioClip[] _clip;

    private void Awake()
    {
        _as = GameObject.FindObjectOfType<SoundManager>().GetComponent<AudioSource>();
    }

    public void PlayerAudioWLoad()
    {
        int randomIndex = Random.Range(0, _clip.Length);
        _as.PlayOneShot(_clip[randomIndex]);
    }
}
