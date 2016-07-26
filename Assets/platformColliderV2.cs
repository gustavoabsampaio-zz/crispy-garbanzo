using UnityEngine;
using System.Collections;

public class platformColliderV2 : MonoBehaviour {

    private BoxCollider2D playerCollider; [SerializeField]
    private BoxCollider2D platformCollider; [SerializeField]
    private Rigidbody2D playerSpeed; [SerializeField]

    void Start ()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        playerSpeed = GameObject.Find("Player").GetComponent<Rigidbody2D>();

    }

    void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            if (playerSpeed.velocity.y > 0)
            {
                Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
            }
            if (playerSpeed.velocity.y < 0)
            {
                Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
            }
        }
    }
}
