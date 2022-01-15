using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public float health = 100;
    public int reward = 20;

    private Transform target;
    private int wavwpointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        WaveSpawner.EnemyAlive--;
        PlayerStats.Money += reward;
        Destroy(gameObject);
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) 
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavwpointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavwpointIndex++;
        target = Waypoints.points[wavwpointIndex]; 
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemyAlive--;
        Destroy(gameObject);
    }
}
