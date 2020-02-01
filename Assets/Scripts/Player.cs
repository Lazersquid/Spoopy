using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 5f;
    public float smoothing = 0.07f;

    private Rigidbody2D body;
    private bool onGround;

    private Transform janitor;

    private Vector2 refVelocity;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        janitor = transform.Find("JanitorSprite");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = SmoothVelocity(new Vector2(-movementSpeed, body.velocity.y));
        } else if (Input.GetKey(KeyCode.D))
        {
            body.velocity = SmoothVelocity(new Vector2(movementSpeed, body.velocity.y));
        } else
        {
            body.velocity = SmoothVelocity(new Vector2(0, body.velocity.y));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (onGround)
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }

        janitor.localScale = new Vector3(-Mathf.Sign(body.velocity.x), 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
    private Vector2 SmoothVelocity(Vector2 target)
    {
        return Vector2.SmoothDamp(body.velocity, target, ref refVelocity, smoothing);
    }
}
