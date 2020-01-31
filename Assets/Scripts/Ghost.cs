using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Rigidbody2D body;
    public float movementSpeed = 5f;

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
            body.velocity = new Vector2(-movementSpeed, body.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = new Vector2(movementSpeed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.velocity = new Vector2(body.velocity.x, movementSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            body.velocity = new Vector2(body.velocity.x, -movementSpeed);
        }
        else
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }
}
