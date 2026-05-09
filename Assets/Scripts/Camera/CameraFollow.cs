using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offset")]
    [SerializeField]
    private Vector3 offset =
        new Vector3(0f, 4f, -10f);

    [Header("Follow Settings")]
    [SerializeField] private float followSmoothness = 5f;

    [SerializeField] private float rotationSmoothness = 5f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition =
            target.position +
            target.TransformDirection(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSmoothness * Time.deltaTime
        );

        Quaternion targetRotation =
            Quaternion.LookRotation(
                target.position - transform.position
            );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSmoothness * Time.deltaTime
        );
    }
}