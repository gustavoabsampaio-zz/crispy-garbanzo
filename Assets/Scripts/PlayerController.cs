using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
	public Text healthText;
    public Text moneyText;
    public float playerHealth;

    private bool attack = false;
	private Rigidbody2D rb2d;
    private bool grounded = false;
    private Animator anim;
	private int playerMoney;
    private bool moving = false;



    void Awake()
	{
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        playerMoney = 0;
        playerHealth = 100;
        MoneyCounter();
        HealthCounter();
    }

    void Update ()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        HandleInput();
        HandleDeath();
    }

	void FixedUpdate()
	{
        float h = Input.GetAxis("Horizontal");
        HandleAnimations();
        HandleMovement();
        ResetValues();
        anim.SetFloat("Speed", Mathf.Abs(h));
    }

    void HandleDeath()
    {
        if (playerHealth <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        //anim.SetFloat("Speed", Mathf.Abs(h));
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (h * rb2d.velocity.x < maxSpeed)
            {
                rb2d.AddForce(Vector2.right * h * moveForce);
                moving = true;
                
            }
            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            {
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
                moving = true;
            }
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();
            if (!Input.anyKey && grounded)
            {
                rb2d.velocity = Vector2.zero;
                moving = false;
            }
            if (jump)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
            
        }
    }

    void HandleInput()
    {
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                jump = true;
            }
            if (Input.GetButtonDown("Attack1") && grounded)
            {
                attack = true;
            }
        }
    }

    void Flip()
    {
            facingRight = !facingRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
    }

    private void HandleAnimations()
    {
        if (attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetTrigger("Attack");
            rb2d.velocity = Vector2.zero;
        }
        if (moving && !jump)
        {
            anim.SetBool("Run",true);
}
        if (!moving)
        {
            anim.SetBool("Run",false);
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("PickUps"))
		{
			other.gameObject.SetActive (false);
			playerMoney += 1;
            MoneyCounter();
        }
        if (other.gameObject.CompareTag("DamageDealer"))
        {
            playerHealth -= Random.Range(14,23);
            HealthCounter();
        }
    }

	void MoneyCounter ()
	{
        moneyText.text = playerMoney.ToString() + "D$";
    }

    void HealthCounter ()
    {
        healthText.text = playerHealth.ToString() + "HP";
    }

    private void ResetValues ()
    {
        attack = false;
    }
}