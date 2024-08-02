using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource DeathSource;
    [SerializeField] AudioSource PJumpSource;

    [Header("----------Audio Clips---------")]
    public AudioClip bgm;
    public AudioClip suppliesPickup;
    public AudioClip o2Refill;
    public AudioClip scrap;
    public AudioClip run;
    public AudioClip slide;
    public AudioClip land;
    public AudioClip PJump;

    [Header("----------Death Sound---------")]
    public AudioClip death;

    private void Start() 
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayDeathSfx()
    {
        SFXSource.PlayOneShot(death);
    }

    public void PlayPJumpSfx()
    {
        PJumpSource.PlayOneShot(PJump);
    }

    /*public void PlayLandSfx()
    {
        SFXSource.PlayOneShot(land);
    }*/

}
