using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayAudioLoaded : MonoBehaviour
{
    AudioSource _as;

    [SerializeField] AudioClip[] _clip;

    Button button;

    private void Awake()
    {
        _as = GameObject.FindObjectOfType<SoundManager>().GetComponent<AudioSource>();

        if(this.gameObject.GetComponent<Button>() != null)
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(PlayerAudioWLoad);
        }

    }

    public void PlayerAudioWLoad()
    {
        int randomIndex = Random.Range(0, _clip.Length);
        _as.PlayOneShot(_clip[randomIndex]);
    }
}
