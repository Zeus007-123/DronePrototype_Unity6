using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    [SerializeField] private float hoverAmplitude = 0.25f;
    [SerializeField] private float hoverFrequency = 2f;

    private Vector3 startPos;
    private float randomOffset;

    private void Start()
    {
        startPos = transform.localPosition;

        randomOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        Vector3 hoverPos = startPos;

        hoverPos.y += Mathf.Sin(Time.time * hoverFrequency + randomOffset)
                      * hoverAmplitude;

        transform.localPosition = hoverPos;
    }
}