﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 5f;

    private Rigidbody2D body;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-movementSpeed, body.velocity.y);
        } else if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(movementSpeed, body.velocity.y);
        } else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (onGround)
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }
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
}
