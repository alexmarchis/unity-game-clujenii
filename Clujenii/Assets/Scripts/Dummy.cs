using UnityEngine;

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

    public void Hit(int direction)
    {
        anim.SetTrigger("Hit");
        Flip(direction);
    }

    void Flip(int direction)
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x) * direction;
        transform.localScale = theScale;
    }
}
