using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDispenser : MonoBehaviour, IAffectable
{
    [SerializeField] bool spawnWithFuseOn = false;
    [SerializeField] Vector2 spawnDirection;
    [SerializeField] float force;

    [SerializeField]
    GameObject bombPrefab;

    private void Awake()
    {
        spawnDirection = spawnDirection.normalized;
    }

    public void OnActivate()
    {
        SpawnBomb();
    }
    private void SpawnBomb()
    {
        GameObject obj = Instantiate(bombPrefab, transform.position + new Vector3(spawnDirection.x, spawnDirection.y, 0), Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(spawnDirection * force, ForceMode2D.Impulse);

        if (spawnWithFuseOn)
        {
            Bomb bomb = obj.GetComponent<Bomb>();
            bomb.TriggerFuse();
        }
    }
}
