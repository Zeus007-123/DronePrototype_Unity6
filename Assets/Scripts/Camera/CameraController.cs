using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject followCamera;
    [SerializeField] private GameObject fpvCamera;
    
    private void Start()
    {
        ActivateChaseCamera();
    }
    
    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (followCamera.activeSelf)
            {
                ActivateFpvCamera();
            }
            else
            {
                ActivateChaseCamera();
            }
        }
    }
    
    private void ActivateChaseCamera()
    {
        followCamera.SetActive(true);
        fpvCamera.SetActive(false);
    }

    private void ActivateFpvCamera()
    {
        followCamera.SetActive(false);
        fpvCamera.SetActive(true);
    }
    
}
