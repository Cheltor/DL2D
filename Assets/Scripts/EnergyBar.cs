using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    public Image energyBarFill;  // Reference to the UI image for the energy bar's fill
    public TextMeshProUGUI energyText;  // Reference to the UI text for the energy value
    public float maxEnergy = 200f;  // Maximum energy value
    private float currentEnergy;  // Current energy value

    void Start()
    {
        // Ensure the energyBarFill and energyText are not null
        if (energyBarFill == null)
        {
            Debug.LogError("EnergyBarFill is not assigned in the Inspector!");
            return;
        }

        if (energyText == null)
        {
            Debug.LogError("EnergyText is not assigned in the Inspector!");
            return;
        }

        // Start with full energy
        currentEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    // Call this function to decrease energy
    public void DecreaseEnergy(float amount)
    {
        currentEnergy -= amount;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        UpdateEnergyBar();
    }

    // Get the current energy value
    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    // Update the energy bar UI
    private void UpdateEnergyBar()
    {
        if (energyBarFill != null)
        {
            energyBarFill.fillAmount = currentEnergy / maxEnergy;  // Set the fill amount (0 to 1)
        }

        if (energyText != null)
        {
            energyText.text = Mathf.Ceil(currentEnergy).ToString();  // Update the text to show current energy (rounded)
        }
        else
        {
            Debug.LogError("EnergyText became null during UpdateEnergyBar.");
        }
    }

}
