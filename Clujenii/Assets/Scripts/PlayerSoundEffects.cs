using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour {

    public AudioClip RunLoop;
    public AudioClip Jump;
    public AudioClip LandJump;
    public AudioClip[] JumpGrunts;
    public AudioClip[] LandGrunts;
    public AudioClip DivineRay;
    public AudioClip[] Punches;

    public float jumpGruntVolume = 1.0f;
    public float punchVolume = 1.0f;
    public float divineRayVolume = 1.0f;

    private AudioSource runAudioSource;
    private AudioSource divineRayAudioSource;
    private AudioSource fxAudioSource;

    private bool isRunning = false;

    void Awake () {
        AudioSource[] sources = GetComponents<AudioSource>();
        runAudioSource = sources[0];
        divineRayAudioSource = sources[1];
        fxAudioSource = sources[2];

        runAudioSource.clip = RunLoop;
        divineRayAudioSource.clip = DivineRay;
        divineRayAudioSource.volume = divineRayVolume;
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

    public void StartDivineRaySound()
    {
        divineRayAudioSource.Play();
    }

    public void StopDivineRaySound()
    {
        divineRayAudioSource.Stop();
    }

    public void PunchSound()
    {
        int punchIndex = Random.Range(0, Punches.Length);
        fxAudioSource.PlayOneShot(Punches[punchIndex], punchVolume);
    }

    public void JumpSound()
    {
        fxAudioSource.PlayOneShot(Jump, 1.0f);
        int jumpGruntIndex = Random.Range(0, JumpGrunts.Length);
        fxAudioSource.PlayOneShot(JumpGrunts[jumpGruntIndex], jumpGruntVolume);
    }

    public void LandJumpSound()
    {
        int landGruntIndex = Random.Range(0, LandGrunts.Length);
        fxAudioSource.PlayOneShot(LandGrunts[landGruntIndex], jumpGruntVolume);
        fxAudioSource.PlayOneShot(LandJump, 1.0f);
    }
}
