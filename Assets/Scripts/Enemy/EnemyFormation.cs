using System;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [Header("Formation")]
    public int rows = 4;
    public int columns = 8;
    public float spacing = 1.5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 4f;
    [SerializeField] private float descendAmount = 0.5f;

    /*[Header("Hover")]
    [SerializeField] private float hoverAmplitude = 0.3f;
    [SerializeField] private float hoverFrequency = 2f;*/

    private Vector3 startPosition;
    private int moveDirection = 1;

    private void Start()
    {
        startPosition = transform.position;

        SpawnFormation();
    }

    private void Update()
    {
        HandleMovement();
        //HandleHover();
    }

    private void HandleMovement()
    {
        transform.Translate(
            Vector3.right *
            moveDirection *
            moveSpeed *
            Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            moveDirection *= -1;

            transform.position += Vector3.back * descendAmount;
        }
    }

    /*private void HandleHover()
    {
        Vector3 hoverPos = transform.position;

        hoverPos.y =
            Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;

        transform.position = hoverPos;
    }*/

    private void SpawnFormation()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 spawnPos =
                    transform.position +
                    new Vector3(
                        (col - (columns - 1) / 2f) * spacing,
                        0,
                        row * spacing
                    );

                Instantiate(
                    enemyPrefab,
                    spawnPos,
                    Quaternion.identity,
                    transform
                );
            }
        }
    }

}