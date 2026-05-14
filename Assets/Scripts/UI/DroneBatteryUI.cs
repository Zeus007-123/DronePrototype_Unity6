using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DroneBatteryUI : MonoBehaviour
{
    [Header("Battery Settings")]
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float baseDrainRate = 0.04f;
    [SerializeField] private float climbDrainRate = 0.083f;
    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private Image batteryFillBar;
    [SerializeField] private CanvasGroup lowBatteryPanel;

    private float _currentBatteryLevel;
    
    private void Start()
    {
        _currentBatteryLevel = maxBattery;
        if (lowBatteryPanel != null) lowBatteryPanel.alpha = 0f;
    }
    
    private void Update()
    {
        if (_currentBatteryLevel <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        float currentDrainRate = Keyboard.current[Key.Q].isPressed ? climbDrainRate : baseDrainRate;
        _currentBatteryLevel -= currentDrainRate * Time.deltaTime;
        _currentBatteryLevel = Mathf.Clamp(_currentBatteryLevel, 0f, maxBattery);

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        float batteryRatio = _currentBatteryLevel / maxBattery;

        if (batteryFillBar != null)
        {
            batteryFillBar.fillAmount = batteryRatio;
            
            batteryFillBar.color = Color.Lerp(Color.red, Color.green, batteryRatio);
        }

        if (batteryText != null)
        {
            batteryText.text = $"BATTERY % : {Mathf.CeilToInt(batteryRatio * 100)}%";
        }

        if (batteryRatio <= 0.2f && lowBatteryPanel != null)
        {
            lowBatteryPanel.alpha = Mathf.PingPong(Time.time * 2f, 0.4f) + 0.1f;
        }
    }

    public bool IsDead()
    {
        return _currentBatteryLevel <= 0f;
    }
}
