using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -12f);

    [Header("Smoothness")]
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 desiredPosition =
            target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        Quaternion targetRotation =
    Quaternion.LookRotation(
        target.position - transform.position
    );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }
}