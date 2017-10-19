using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour {

    public AudioClip RunLoop;
    public AudioClip Jump;
    public AudioClip LandJump;

    private AudioSource runAudioSource;
    private AudioSource fxAudioSource;

    private bool isRunning = false;

    void Awake () {
        AudioSource[] sources = GetComponents<AudioSource>();
        runAudioSource = sources[0];
        fxAudioSource = sources[1];

        runAudioSource.clip = RunLoop;
    }

    public void StartRunSound()
    {
        if (!isRunning)
        {
            isRunning = true;
            runAudioSource.Play();
        }
    }

    public void StopRunSound()
    {
        if (isRunning)
        {
            isRunning = false;
            runAudioSource.Stop();
        }
    }

    public void JumpSound()
    {
        fxAudioSource.PlayOneShot(Jump, 1.0f);
    }

    public void LandJumpSound()
    {
        fxAudioSource.PlayOneShot(LandJump, 1.0f);
    }
}
