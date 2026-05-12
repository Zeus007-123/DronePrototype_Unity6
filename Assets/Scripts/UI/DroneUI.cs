using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DroneUI : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI proximityText;
    [SerializeField] private Image warningPanel;
    [SerializeField] private float warningThreshold = 3f;

    private void Update()
    {
        if (playerController == null)
            return;

        float speed = playerController.velocity.magnitude * 3.6f;
        float height = playerController.pos.y;
        float distance = playerController.proximityDistance;

        velocityText.text = "Velocity: " + speed.ToString("F1") + " km/h";
        heightText.text = "Altitude: " + height.ToString("F1") + "m";

        if(distance != -1)
        {
            proximityText.text = "Obstacle At: " + distance.ToString("F1") + "m";
            warningPanel.color = distance < warningThreshold ? Color.red : Color.clear;
        }
        else
        {
            proximityText.text = "Path Ahead Clear";
            warningPanel.color = Color.clear;
        }

    }
}
