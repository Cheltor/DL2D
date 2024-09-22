using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    public PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    public TextMeshProUGUI speedText;  // Reference to the TextMeshPro UI element

    void Update()
    {
        // Check if the playerMovement and speedText references are set
        if (playerMovement != null && speedText != null)
        {
            // Get the player's current vertical speed from the PlayerMovement script
            float currentSpeed = playerMovement.GetCurrentSpeed();

            // Update the SpeedText with the current speed, rounded to 1 decimal place
            speedText.text = "Speed: " + currentSpeed.ToString("F1");
        }
    }
}
