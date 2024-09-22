using UnityEngine;
using TMPro;

public class DistanceToFinishLine : MonoBehaviour
{
    public Transform player;  // Reference to the player Transform
    public Transform startLine;  // Reference to the start line Transform
    public Transform finishLine;  // Reference to the finish line Transform
    public TextMeshProUGUI progressText;  // Reference to the UI Text for showing progress

    private float totalDistanceY;  // Total vertical distance from start to finish

    void Start()
    {
        // Calculate the total vertical distance (Y-axis) between the start line and finish line
        totalDistanceY = Mathf.Abs(finishLine.position.y - startLine.position.y);
    }

    void Update()
    {
        // Calculate the current vertical distance (Y-axis) between the player and the finish line
        float playerDistanceY = Mathf.Abs(finishLine.position.y - player.position.y);

        // Calculate the remaining progress based on the player's Y-axis distance from the finish line
        float remainingProgress = playerDistanceY / totalDistanceY;  // 0 means finish line reached

        // Convert remaining progress to a percentage (0 to 100%)
        remainingProgress = Mathf.Clamp01(remainingProgress) * 100;

        // Update the progress text on the HUD
        progressText.text = "Remaining: " + remainingProgress.ToString("F1") + "%";
    }
}
