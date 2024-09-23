using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCompetitor : MonoBehaviour
{
    public float verticalSpeed = 3f;  // Constant vertical speed
    private Rigidbody2D rb;

    public Transform finishLine;  // Reference to the finish line (optional, for collision checks)

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Make the Rigidbody kinematic so it won't react to physics forces
        rb.isKinematic = false;

        // Optional: You can also freeze position or rotation if you need additional control
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;  // Prevent any rotation if collision occurs
    }

    void FixedUpdate()
    {
        // Apply constant upward movement with the specified vertical speed
        rb.velocity = new Vector2(0f, verticalSpeed);  // Only move in the Y-axis
    }

    // Optional: Detect if the competitor reaches the finish line
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("Test Competitor reached the finish line!");
            // Add additional logic here for what happens when the AI reaches the finish line
        }
    }
}
