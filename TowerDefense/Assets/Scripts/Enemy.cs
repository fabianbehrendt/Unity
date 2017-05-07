using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform destination;


    private NavMeshAgent agent;
    private float health = 100f;
    private float speed = 3.5f;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        agent.SetDestination(destination.position);
        agent.speed = speed;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
