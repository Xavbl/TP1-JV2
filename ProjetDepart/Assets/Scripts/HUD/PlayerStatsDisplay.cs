using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    [Header("Text display")]
    [SerializeField] private TextMeshProUGUI healthPointsDisplay;
    [SerializeField] private TextMeshProUGUI missilesDisplay;

    private int healthPoints;
    private int nbMissiles;

    private void Awake()
    {
        healthPointsDisplay = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        missilesDisplay = GameObject.Find("MissilesText").GetComponent<TextMeshProUGUI>();
        EventChannels.OnPlayerHealthChange += UpdateHealth;
        EventChannels.OnNumberMissileChange += UpdateMissilesCount;
    }

    void UpdateHealth(int health)
    {
        healthPointsDisplay.text = healthPoints.ToString();
    }

    void UpdateMissilesCount(int nbMissiles)
    {
        missilesDisplay.text = nbMissiles.ToString();
    }
}
