using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float smoothing = 0.1f;

    private Rigidbody2D body;
    private Vector2 refVelocity;

    private Vector2 lowerLeft;
    private Vector2 upperRight;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, lowerLeft.x, upperRight.x),
            Mathf.Clamp(transform.position.y, lowerLeft.y, upperRight.y));
    }

    private Vector2 SmoothVelocity(Vector2 target)
    {
        return Vector2.SmoothDamp(body.velocity, target, ref refVelocity, smoothing);
    }
}
