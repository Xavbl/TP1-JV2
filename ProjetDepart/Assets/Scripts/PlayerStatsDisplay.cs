using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    //[Header("Image display")]
    //[SerializeField] private Texture2D heartHUDTexture;
    //[SerializeField] private Texture2D missilesHUDTexture;

    [Header("Text display")]
    [SerializeField] private TextMeshProUGUI healthPointsDisplay;
    [SerializeField] private TextMeshProUGUI missilesDisplay;

    private int healthPoints;
    private int nbMissiles;

    // Pour afficher HP et missiles en temps réel
    // Utiliser notifier / canal / finders pour
    // envoyer "ping" et changer les stats.


    private void Awake()
    {
        healthPointsDisplay = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        missilesDisplay = GameObject.Find("MissilesText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        healthPointsDisplay.text = healthPoints.ToString();
        missilesDisplay.text = nbMissiles.ToString();
    }
}
