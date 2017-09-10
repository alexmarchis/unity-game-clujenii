using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioMixerSnapshot volumeDown;           //Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
    public AudioMixerSnapshot volumeUp;             //Reference to Audio mixer snapshot in which the master volume of main mixer is turned up

    private AudioSource musicSource;                //Reference to the AudioSource which plays music
    private float resetTime = .01f;                 //Very short time used to fade in near instantly without a click

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayLevelMusic()
    {
        FadeUp(resetTime);
        musicSource.Play();
    }

    public void FadeUp(float fadeTime)
    {
        volumeUp.TransitionTo(fadeTime);
    }

    public void FadeDown(float fadeTime)
    {
        volumeDown.TransitionTo(fadeTime);
    }
}
