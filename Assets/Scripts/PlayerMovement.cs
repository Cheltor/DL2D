using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float[] speeds = new float[5];  // Array to store 5 different vertical speeds
    public float horizontalSpeed = 5f;  // Speed of the player moving left and right
    public float smoothing = 0.5f;  // Smoothing factor for the player's movement
    public float smoothAccelerationTime = 0.2f;  // Time to smooth acceleration/deceleration
    public float energyDrainRate = 1f;  // Rate at which energy is drained
    private Rigidbody2D rb; 
    private float targetXPosition;  // Target X position for the player
    private float velocityX = 0f;  // Velocity of the player in the X direction

    private float currentVerticalSpeed = 0f;  // Current vertical speed
    private float verticalSpeedVelocity = 0f;  // Used by SmoothDamp for smooth vertical speed transitions

    public EnergyBar energyBar;  // Reference to the EnergyBar script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component from the player

        // Initialize speeds: 'a' is the lowest speed, spacebar is the highest speed
        speeds[0] = 2f;  // 'a'
        speeds[1] = 3f;  // 's'
        speeds[2] = 4f;  // 'd'
        speeds[3] = 5f;  // 'f'
        speeds[4] = 6f;  // spacebar
    }

    void FixedUpdate()
    {
        // Get the highest speed based on key presses
        float targetVerticalSpeed = GetMaxSpeed();

        // Smooth the transition between the current speed and the target speed using SmoothDamp
        currentVerticalSpeed = Mathf.SmoothDamp(currentVerticalSpeed, targetVerticalSpeed, ref verticalSpeedVelocity, smoothAccelerationTime);

        // Constant upward movement with smoothed vertical speed
        rb.velocity = new Vector2(rb.velocity.x, currentVerticalSpeed);

        // Get the mouse position relative to the screen and convert it to world position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Smooth mouse position to prevent sudden jumps
        Vector3 smoothedMouseWorldPosition = Vector3.Lerp(rb.position, mouseWorldPosition, smoothing);
        targetXPosition = smoothedMouseWorldPosition.x;

        // Smoothly interpolate the player's X position towards the target X position
        float smoothX = Mathf.SmoothDamp(rb.position.x, targetXPosition, ref velocityX, smoothing);

        // Set the new velocity with the smoothed X movement
        rb.velocity = new Vector2((smoothX - rb.position.x) * horizontalSpeed, rb.velocity.y);

        // Drain energy over time as the player moves upward
        if (rb.velocity.y > 0)  // If the player is moving up
        {
            energyBar.DecreaseEnergy(energyDrainRate * Time.deltaTime);  // Decrease energy over time
        }
    }

    // Get the highest speed based on key presses
    private float GetMaxSpeed()
    {
        float maxSpeed = 0f;

        // Check for key presses and set the max speed
        if (Input.GetKey(KeyCode.A))
        {
            maxSpeed = Mathf.Max(maxSpeed, speeds[0]);  // 'a'
        }
        if (Input.GetKey(KeyCode.S))
        {
            maxSpeed = Mathf.Max(maxSpeed, speeds[1]);  // 's'
        }
        if (Input.GetKey(KeyCode.D))
        {
            maxSpeed = Mathf.Max(maxSpeed, speeds[2]);  // 'd'
        }
        if (Input.GetKey(KeyCode.F))
        {
            maxSpeed = Mathf.Max(maxSpeed, speeds[3]);  // 'f'
        }
        if (Input.GetKey(KeyCode.Space))
        {
            maxSpeed = Mathf.Max(maxSpeed, speeds[4]);  // spacebar
        }

        return maxSpeed;
    }

    // Expose the current speed to other scripts
    public float GetCurrentSpeed()
    {
        return currentVerticalSpeed;
    }

    // Detect player crossing the start or finish lines
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StartLine"))
        {
            Debug.Log("Player crossed the start line.");
        }

        if (collision.gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("Player crossed the finish line.");
        }
    }
}
