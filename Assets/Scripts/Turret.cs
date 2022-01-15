using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;

    [Header("Use bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;

    [Header("Unity setup fields")]

    public string enemyTag = "Enemy";
    public Transform turret;
    public float turnSpeed = 10f;

    public Transform firePoint;
    
    
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                    lineRenderer.enabled = false;
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();            
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }

            fireCountdown -= Time.deltaTime;    
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime*turnSpeed).eulerAngles;
        turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        Enemy e = target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(0.5f);// default for laser
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag(enemyTag);
        Debug.Log(enemys.Length);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemys)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log(bulletPrefab);
        Debug.Log(bulletGO);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        Debug.Log(bullet);

        Debug.Log("SHOOT!");

        if (bullet != null)
        {
            Debug.Log("BULLET!");
            bullet.Seek(target);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
