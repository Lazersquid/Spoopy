using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 5f;
    public float smoothing = 0.07f;

    public float groundRaySpacing;
    public Vector2 groundRayOffset;
    public float groundRayLength;
    
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
            else
            {
                var ray1Pos = (Vector2)transform.position + groundRayOffset + (Vector2.right * groundRaySpacing) / 2;
                var ray2Pos = (Vector2)transform.position + groundRayOffset - (Vector2.right * groundRaySpacing) / 2;
                if (ShootGroundedRay(ray1Pos, Vector2.down, groundRayLength)
                    || ShootGroundedRay(ray2Pos, Vector2.down, groundRayLength))
                    body.velocity = new Vector2(body.velocity.x, jumpSpeed);
                    
            }
        }

        janitor.localScale = new Vector3(-Mathf.Sign(body.velocity.x), 1f, 1f);
    }

    private bool ShootGroundedRay(Vector2 origin, Vector2 direction, float distance)
    {
        var results = Physics2D.RaycastAll(origin, direction, distance);
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].collider.CompareTag("Ground"))
                return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        var ray1Pos = (Vector2)transform.position + groundRayOffset + (Vector2.right * groundRaySpacing) / 2;
        var ray2Pos = (Vector2)transform.position + groundRayOffset - (Vector2.right * groundRaySpacing) / 2;
        Debug.DrawLine(ray1Pos, ray1Pos + Vector2.down * groundRayLength, Color.blue);
        Debug.DrawLine(ray2Pos, ray2Pos + Vector2.down * groundRayLength, Color.blue);
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
