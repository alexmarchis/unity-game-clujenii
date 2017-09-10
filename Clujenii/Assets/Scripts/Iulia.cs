using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iulia : MonoBehaviour {

    private GameObject instructions;
    // Use this for initialization
    void Awake () {
        instructions = GameObject.Find("Instructions");
        instructions.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            instructions.SetActive(true);
            Flip(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            instructions.SetActive(false);
            Flip(other);
        }
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
