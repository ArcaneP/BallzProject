using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _as;

    [SerializeField] AudioClip[] succes_clip;
    [SerializeField] AudioClip[] defeat_clip;
    [SerializeField] AudioClip[] losehp_clip;

    public static SoundManager Instance { get; private set; }

    private float timer = 5f;  

    private void Awake()
    {

        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }  

    public void PlayerWinSFX()
    {
        int randomIndex = Random.Range(0, succes_clip.Length);
        _as.PlayOneShot(succes_clip[randomIndex]);
    }

    public void PlayDefeatSFX()
    {
        int randomIndex = Random.Range(0, defeat_clip.Length);
        _as.PlayOneShot(defeat_clip[randomIndex]);
    }

    public void PlayLoseHPSFX()
    {
        int randomIndex = Random.Range(0, losehp_clip.Length);
        _as.PlayOneShot(losehp_clip[randomIndex]);
    }

}
