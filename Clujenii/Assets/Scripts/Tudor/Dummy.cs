using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int InitalEvilness = 100;

    private int evilness;
    private bool isCured = false;

    private Animator anim;					// Reference to the player's animator component.
    private BoxCollider2D boxCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        evilness = InitalEvilness;
    }

    void Update()
    {

    }

    public void Hit(int direction)
    {
        anim.SetTrigger("Hit");
        anim.SetBool("Cured", false);
        evilness = InitalEvilness;
        isCured = false;
        Flip(direction);
    }

    public void Cure()
    {
        if (evilness > 0)
        { 
            evilness--;
        }
        if(evilness == 0)
        {
            isCured = true;
            anim.SetBool("Cured", true);
        }
    }

    void Flip(int direction)
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x) * direction;
        transform.localScale = theScale;
    }

    public bool IsCured()
    {
        return isCured;
    }
}
