using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    [Header("Text display")]
    [SerializeField] private TextMeshProUGUI healthPointsDisplay;
    [SerializeField] private TextMeshProUGUI missilesDisplay;
    [SerializeField] private TextMeshProUGUI winMessageDisplay;
    [SerializeField] private TextMeshProUGUI loseMessageDisplay;


    private void Awake()
    {
        healthPointsDisplay = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        missilesDisplay = GameObject.Find("MissilesText").GetComponent<TextMeshProUGUI>();
        winMessageDisplay = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        loseMessageDisplay = GameObject.Find("LoseText").GetComponent<TextMeshProUGUI>();
        winMessageDisplay.enabled = false;
        loseMessageDisplay.enabled = false;
        EventChannels.OnPlayerHealthChange += UpdateHealth;
        EventChannels.OnNumberMissileChange += UpdateMissilesCount;
        EventChannels.OnWin += ShowVictoryScreen;
        EventChannels.OnPlayerDeath += ShowGameOverScreen;
    }

    void UpdateHealth(int health)
    {
        healthPointsDisplay.text = health.ToString();
    }

    void UpdateMissilesCount(int nbMissiles)
    {
        missilesDisplay.text = nbMissiles.ToString();
    }

    void ShowVictoryScreen()
    {
        winMessageDisplay.enabled = true;
        Time.timeScale = 0;
    }

    void ShowGameOverScreen()
    {
        loseMessageDisplay.enabled = true;
    }
}
