using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject rotatable;
    public GameObject bulletPrefab;
    public Transform barrelEnd;


    private float shootingSpeed = 2f;
    private float range = 4f;
    private float lastTimeShot;
    private GameObject enemy;
    private List<GameObject> enemiesInRange = new List<GameObject>();


    private void Awake()
    {
        lastTimeShot = -shootingSpeed;
        GetComponent<SphereCollider>().radius = range;
    }


    private void Update()
    {
        Debug.Log(enemiesInRange.Count);

        foreach (GameObject enemyInRange in enemiesInRange)
        {
            if (enemy == null)
            {
                enemy = enemyInRange;
            }
        }

        if (enemy == null)
            return;

        rotatable.transform.LookAt(new Vector3(enemy.transform.position.x, rotatable.transform.position.y, enemy.transform.position.z));

        if (Time.time - lastTimeShot >= shootingSpeed)
        {
            lastTimeShot = Time.time;

            GameObject bullet = Instantiate(bulletPrefab, barrelEnd.position, barrelEnd.rotation) as GameObject;
            bullet.GetComponent<Bullet>().destination = enemy.transform;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.gameObject);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);

            if (enemy == other.gameObject)
                enemy = null;
        }
    }
}
