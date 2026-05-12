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
    [SerializeField] private float pitchAmount = 10f;
    [SerializeField] private float rotationSmoothness = 5f;
    [SerializeField] private float yawRotationSpeed = 5f;

    [Header("Hover")]
    [SerializeField] private float hoverAmplitude = 0.15f;
    [SerializeField] private float hoverFrequency = 2f;

    [Header("Bounds")]
    [SerializeField] private float horizontalLimit = 40f;
    [SerializeField] private float forwardLimit = 40f;
    [SerializeField] private float minHeight = 2f;
    [SerializeField] private float maxHeight = 20f;

    [Header("Collision Detection")]
    [SerializeField] private float sensorRange = 10f;
    [SerializeField] private LayerMask obstacleLayer;
    public float proximityDistance;

    public Vector3 velocity;

    private DroneControls controls;

    private Vector2 moveInput;
    public Vector3 pos;
    private float heightInput;

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
        CollisionDetection();
        //HandleShooting();
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

        pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -horizontalLimit, horizontalLimit);

        pos.z = Mathf.Clamp(pos.z, -forwardLimit, forwardLimit);

        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);

        transform.position = pos;
    }

    private void HandleRotation()
    {
        Vector3 flatVelocity =
            new Vector3(
                velocity.x,
                0f,
                velocity.z
            );

        if (flatVelocity.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation =
                Quaternion.LookRotation(flatVelocity.normalized);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                lookRotation,
                yawRotationSpeed * Time.deltaTime
            );
        }

        float horizontalTilt =
            -moveInput.x * tiltAmount;

        float forwardTilt =
            moveInput.y * -pitchAmount;

        Quaternion tiltRotation =
            Quaternion.Euler(
                forwardTilt,
                transform.eulerAngles.y,
                horizontalTilt
            );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            tiltRotation,
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

    private void CollisionDetection()
    {
        RaycastHit hit;
        Vector3 direction = transform.forward;
        float sphereRadius = 2.0f;

        if(Physics.SphereCast(transform.position, sphereRadius, direction, out hit, sensorRange, obstacleLayer)) 
        {
            proximityDistance = hit.distance;
        }
        else
        {
            proximityDistance = -1;
        }
    }

    
}