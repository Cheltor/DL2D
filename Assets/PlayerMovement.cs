using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player moving forward
    public float horizontalSpeed = 5f; // Speed of the player moving left and right

    private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the player
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed); // Move the player forward

        float horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal input from the player
        rb.velocity = new Vector2(horizontalInput * horizontalSpeed, rb.velocity.y); // Move the player left and right
    }
}
