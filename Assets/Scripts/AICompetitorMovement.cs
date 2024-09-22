using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompetitorMovement : MonoBehaviour
{
    public float[] speeds = new float[5];  // Array to store 5 different vertical speeds
    public float horizontalSpeed = 5f;  // Speed of the AI moving left and right
    public float smoothing = 0.5f;  // Smoothing factor for the AI's movement
    public float smoothAccelerationTime = 0.2f;  // Time to smooth acceleration/deceleration
    public float baseEnergyDrainRate = 0.5f;  // Base energy drain rate for the slowest speed
    public float energyExponent = 2f;  // Exponent to increase energy drain exponentially with speed
    public float decisionInterval = 2f;  // Interval for AI to change speed
    private Rigidbody2D rb; 

    private float currentVerticalSpeed = 0f;  // Current vertical speed
    private float verticalSpeedVelocity = 0f;  // Used by SmoothDamp for smooth vertical speed transitions
    private float energy = 100f;  // AI energy (can be changed depending on difficulty)
    private float lastDecisionTime;  // Time for AI's last decision

    public Transform finishLine;  // AI's target, the finish line

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component from the AI

        // Initialize speeds: 'a' is the lowest speed, spacebar is the highest speed
        speeds[0] = 2f;  // Low speed
        speeds[1] = 3f;  // Moderate speed
        speeds[2] = 4f;  // Moderate-high speed
        speeds[3] = 5f;  // High speed
        speeds[4] = 6f;  // Max speed

        // Set initial speed decision
        lastDecisionTime = Time.time;
        MakeSpeedDecision();
    }

    void FixedUpdate()
    {
        // Move the AI upward toward the finish line
        MoveAI();

        // Drain energy as AI moves
        DrainEnergy();

        // Check if the AI needs to make a new speed decision
        if (Time.time > lastDecisionTime + decisionInterval)
        {
            MakeSpeedDecision();
            lastDecisionTime = Time.time;
        }
    }

    // AI makes a speed decision, choosing a random speed based on its remaining energy
    void MakeSpeedDecision()
    {
        if (energy > 20f)  // AI only selects faster speeds if it has enough energy
        {
            currentVerticalSpeed = speeds[Random.Range(2, 5)];  // Randomly select from higher speeds
        }
        else if (energy > 10f)
        {
            currentVerticalSpeed = speeds[Random.Range(1, 4)];  // Randomly select from moderate speeds
        }
        else
        {
            currentVerticalSpeed = speeds[0];  // Move slowly if energy is low
        }
    }

    // Move the AI with smooth acceleration
    void MoveAI()
    {
        // Smooth the AI's movement using SmoothDamp
        float smoothY = Mathf.SmoothDamp(rb.velocity.y, currentVerticalSpeed, ref verticalSpeedVelocity, smoothAccelerationTime);

        // Constant upward movement with smoothed vertical speed
        rb.velocity = new Vector2(rb.velocity.x, smoothY);

        // Optional: You could add AI horizontal movement or make it follow the player if needed
    }

    // Drain energy based on current speed
    void DrainEnergy()
    {
        if (currentVerticalSpeed > 0)
        {
            // Calculate energy drain based on current speed, with exponential scaling
            float energyDrain = baseEnergyDrainRate * Mathf.Pow(currentVerticalSpeed, energyExponent);

            // Decrease energy
            energy -= energyDrain * Time.deltaTime;

            // Ensure energy doesn't drop below zero
            energy = Mathf.Max(energy, 0f);
        }
    }

    // Expose current speed for external use
    public float GetCurrentSpeed()
    {
        return currentVerticalSpeed;
    }

    // Optional: Detect AI reaching the finish line
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("AI reached the finish line!");
            // Add logic for what happens when AI reaches the finish line
        }
    }
}
