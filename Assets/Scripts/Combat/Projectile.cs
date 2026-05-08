using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(
    explosionPrefab,
    other.transform.position,
    Quaternion.identity
);

            Destroy(other.gameObject);

            Destroy(gameObject);
        }
    }
}