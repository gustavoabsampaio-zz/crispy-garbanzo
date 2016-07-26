using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : Character
{

    [HideInInspector] public bool jump = false;
    
    public float jumpForce = 1000f;
    public Transform groundCheck;
	public Text healthText;
    public Text moneyText;
    public float health;

    private bool grounded = false;
	private int playerMoney;



    public override void Start()
	{
        base.Start();
        playerMoney = 0;
        health = 100;
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
        if (health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            }
            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            {
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
            }
            if (h > 0 && !facingRight)
                ChangeDirection();
            else if (h < 0 && facingRight)
                ChangeDirection();
            if (h==0 && grounded || attack)
            {
                rb2d.velocity = Vector2.zero;
            }
            if (h==0)
            {
                moving = false;
            }
            else
            {
                moving = true;
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

    private void HandleAnimations()
    {
        if (attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Attack();
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
        if (other.gameObject.CompareTag("DamageFire"))
        {
            health -= Random.Range(14,23);
            HealthCounter();
        }
        if (other.gameObject.CompareTag("DamageGolem"))
        {
            health -= Random.Range(14, 23);
            HealthCounter();
        }
    }

	void MoneyCounter ()
	{
        moneyText.text = playerMoney.ToString() + "D$";
    }

    void HealthCounter ()
    {
        healthText.text = health.ToString() + "HP";
    }

    private void ResetValues ()
    {
        attack = false;
    }
}