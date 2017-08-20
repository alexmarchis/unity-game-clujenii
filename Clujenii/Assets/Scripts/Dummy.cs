using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour
{
    private Animator anim;					// Reference to the player's animator component.
    private BoxCollider2D boxCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }
}
