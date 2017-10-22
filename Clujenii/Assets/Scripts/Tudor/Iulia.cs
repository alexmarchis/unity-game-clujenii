using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iulia : MonoBehaviour {


    public float TalkingVolume = 1.0f;
    private GameObject instructions;
    private AudioSource audioSource;
    private bool isFadingOut = false;
    // Use this for initialization
    void Awake () {
        instructions = GameObject.Find("Instructions");
        instructions.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isFadingOut)
        {
            if (audioSource.volume == 0f
               && audioSource.isPlaying)
            {
                audioSource.Stop();
                isFadingOut = false;
            }
            else
            {
                audioSource.volume -= 0.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            isFadingOut = false;
            audioSource.volume = TalkingVolume;
            audioSource.Play();
            instructions.SetActive(true);
            Flip(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            instructions.SetActive(false);
            isFadingOut = true;
            Flip(other);
        }
    }

    private void StopTalkin()
    {
        audioSource.Stop();
    }

    private void Flip(Collider2D other)
    {
        if (other.transform.position.x < transform.position.x)
        {
            Flip(-1);
        }
        else
        {
            Flip(1);
        }
    }

    private void Flip(int direction)
    {
        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x) * direction;
        transform.localScale = theScale;
    }
}
