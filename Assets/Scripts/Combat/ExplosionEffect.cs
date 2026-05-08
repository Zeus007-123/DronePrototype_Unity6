using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float expandSpeed = 5f;
    [SerializeField] private float destroyTime = 0.3f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
    }
}