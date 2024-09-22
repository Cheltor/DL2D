using UnityEngine;
using TMPro;

public class DistanceToFinishLine : MonoBehaviour
{
    public Transform player;  // Reference to the player Transform
    public Transform startLine;  // Reference to the start line Transform
    public Transform finishLine;  // Reference to the finish line Transform
    public TextMeshProUGUI progressText;  // Reference to the UI Text for showing progress

    private float totalDistance;  // Total distance from start to finish

    void Start()
    {
        // Calculate the total distance between the start line and finish line
        totalDistance = Vector2.Distance(startLine.position, finishLine.position);
    }

    void Update()
    {
        // Calculate the current distance between the player and the finish line
        float playerDistance = Vector2.Distance(player.position, finishLine.position);

        // Calculate the remaining distance percentage based on the player's distance from the finish line
        float remainingProgress = playerDistance / totalDistance;  // 0 means finish line reached

        // Convert remaining progress to a percentage (0 to 100%)
        remainingProgress = Mathf.Clamp01(remainingProgress) * 100;

        // Update the progress text on the HUD
        progressText.text = "Remaining: " + remainingProgress.ToString("F1") + "%";
    }
}
