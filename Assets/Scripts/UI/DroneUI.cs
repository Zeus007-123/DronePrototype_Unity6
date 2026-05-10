/*using UnityEngine;
using TMPro;

public class DroneUI : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI heightText;

    private void Awake()
    {
        velocityText = GetComponent<TextMeshProUGUI>();
        heightText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playerController == null)
            return;

        float speed = playerController.velocity.magnitude;
        float height = playerController.pos.magnitude;

        velocityText.text = "Velocity: " + speed.ToString("F1") + " m/s";
        heightText.text = "Altitude: " + height.ToString("F1") + "m";
    }
}*/
