using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float drag = 3f;

    [Header("Vertical Movement")]
    [SerializeField] private float verticalSpeed = 6f;

    [Header("Rotation")]
    [SerializeField] private float tiltAmount = 20f;
    [SerializeField] private float rotationSmoothness = 5f;

    [Header("Hover")]
    [SerializeField] private float hoverAmplitude = 0.15f;
    [SerializeField] private float hoverFrequency = 2f;

    [Header("Bounds")]
    [SerializeField] private float horizontalLimit = 40f;
    [SerializeField] private float forwardLimit = 40f;
    [SerializeField] private float minHeight = 2f;
    [SerializeField] private float maxHeight = 20f;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;

    private Vector3 velocity;
    private float nextFireTime;

    private DroneControls controls;

    private Vector2 moveInput;
    private float heightInput;
    private bool shootPressed;

    private void Awake()
    {
        controls = new DroneControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += ctx =>
            moveInput = ctx.ReadValue<Vector2>();

        controls.Player.Move.canceled += ctx =>
            moveInput = Vector2.zero;

        controls.Player.Height.performed += ctx =>
            heightInput = ctx.ReadValue<float>();

        controls.Player.Height.canceled += ctx =>
            heightInput = 0f;

        controls.Player.Shoot.performed += ctx =>
            shootPressed = true;

        controls.Player.Shoot.canceled += ctx =>
            shootPressed = false;
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleHover();
        HandleShooting();
    }

    private void HandleMovement()
    {
        Vector3 movementInput =
            (transform.right * moveInput.x) +
            (transform.forward * moveInput.y);

        velocity += movementInput * acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        velocity = Vector3.Lerp(
            velocity,
            Vector3.zero,
            drag * Time.deltaTime
        );

        transform.position += velocity * Time.deltaTime;

        transform.position +=
            Vector3.up * heightInput * verticalSpeed * Time.deltaTime;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -horizontalLimit, horizontalLimit);

        pos.z = Mathf.Clamp(pos.z, -forwardLimit, forwardLimit);

        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);

        transform.position = pos;
    }

    private void HandleRotation()
    {
        float horizontalTilt =
            -moveInput.x * tiltAmount;

        float forwardTilt =
            moveInput.y * -10f;

        Quaternion targetRotation =
            Quaternion.Euler(
                forwardTilt,
                0f,
                horizontalTilt
            );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSmoothness * Time.deltaTime
        );
    }

    private void HandleHover()
    {
        Vector3 hoverPos = transform.position;

        hoverPos.y +=
            Mathf.Sin(Time.time * hoverFrequency)
            * hoverAmplitude
            * Time.deltaTime;

        transform.position = hoverPos;
    }

    private void HandleShooting()
    {
        if (shootPressed &&
            Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Instantiate(
                projectilePrefab,
                firePoint.position,
                firePoint.rotation
            );
        }
    }
}