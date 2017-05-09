using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform destination;


    private float speed = 5;
    private Rigidbody bullet;


    private void Awake()
    {
        bullet = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        if (destination == null)
            Destroy(gameObject);

        bullet.velocity = transform.forward * speed;

        Quaternion targetRotation = Quaternion.LookRotation(destination.position - transform.position);
        bullet.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 20f));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetHashCode() == destination.gameObject.GetHashCode())
        {
            destination.GetComponent<Enemy>().TakeDamage(10f);
            Destroy(gameObject);
        }
    }
}
