using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

    protected float maxSpeed = 5;
    protected float moveForce = 365f;
    [HideInInspector]
    protected bool facingRight = true;
    protected bool attack = false;
    protected Animator anim;
    protected bool moving = false;
    public Rigidbody2D rb2d;

    // Use this for initialization
    public virtual void Start () {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        rb2d.velocity = Vector2.zero;
    }
}
