using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform rotatable;
    public Transform barrelEnd;


    private float shootingSpeed = .5f;
    private float turnSpeed = 20f;
    private float range = 10f;
    private float lastTimeShot;
    private Transform enemy;
    private List<Transform> enemiesInRange = new List<Transform>();


    private void Awake()
    {
        lastTimeShot = -shootingSpeed;
        GetComponent<SphereCollider>().radius = range;
    }


    private void Update()
    {
        foreach (Transform enemyInRange in enemiesInRange)
        {
            if (enemy == null)
            {
                enemy = enemyInRange;
            }
        }

        if (enemy == null)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(enemy.position - rotatable.position);
        rotatable.rotation = Quaternion.RotateTowards(rotatable.rotation, targetRotation, turnSpeed);

        if (Time.time - lastTimeShot >= shootingSpeed && Quaternion.Angle(rotatable.rotation, targetRotation) < 5f)
        {
            lastTimeShot = Time.time;

            GameObject bullet = Instantiate(bulletPrefab, barrelEnd.position, barrelEnd.rotation) as GameObject;
            bullet.GetComponent<Bullet>().destination = enemy.transform;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.transform);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);

            if (enemy == other.gameObject)
                enemy = null;
        }
    }
}
