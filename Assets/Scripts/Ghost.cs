using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float smoothing = 0.1f;

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
            body.velocity = SmoothVelocity(new Vector2(-movementSpeed, body.velocity.y));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = SmoothVelocity(new Vector2(movementSpeed, body.velocity.y));
        }
        else
        {
            body.velocity = SmoothVelocity(new Vector2(0, body.velocity.y));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.velocity = SmoothVelocity(new Vector2(body.velocity.x, movementSpeed));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            body.velocity = SmoothVelocity(new Vector2(body.velocity.x, -movementSpeed));
        }
        else
        {
            body.velocity = SmoothVelocity(new Vector2(body.velocity.x, 0));
        }

        transform.localEulerAngles = new Vector3(0f, 0f, -2 * body.velocity.x);
    }

    private Vector2 SmoothVelocity(Vector2 target)
    {
        return Vector2.SmoothDamp(body.velocity, target, ref refVelocity, smoothing);
    }
}
