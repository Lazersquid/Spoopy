using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody2D body;
    private Vector2 refVelocity;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(-movementSpeed, body.velocity.y), ref refVelocity, 0.1f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(movementSpeed, body.velocity.y), ref refVelocity, 0.1f);
        }
        else
        {
            body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(0, body.velocity.y), ref refVelocity, 0.1f);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(body.velocity.x, movementSpeed), ref refVelocity, 0.1f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(body.velocity.x, -movementSpeed), ref refVelocity, 0.1f);
        }
        else
        {
            body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(body.velocity.x, 0), ref refVelocity, 0.1f);
        }
    }
}
