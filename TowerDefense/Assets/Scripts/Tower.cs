using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject rotatable;
    public GameObject enemy;
    public GameObject bulletPrefab;
    public Transform barrelEnd;


    private float shootingSpeed = 2f;
    private float lastTimeShot;


    private void Awake()
    {
        lastTimeShot = -shootingSpeed;
    }


    private void Update()
    {
        rotatable.transform.LookAt(new Vector3(enemy.transform.position.x, rotatable.transform.position.y, enemy.transform.position.z));

        if (Time.time - lastTimeShot >= shootingSpeed)
        {
            lastTimeShot = Time.time;

            GameObject bullet = Instantiate(bulletPrefab, barrelEnd.position, barrelEnd.rotation) as GameObject;
            bullet.GetComponent<Bullet>().destination = enemy.transform;
        }
    }
}
