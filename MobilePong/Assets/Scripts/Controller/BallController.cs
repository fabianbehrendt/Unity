using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody ball;
    private float speed;
    private Vector3 direction;


    private void Awake()
    {
        ball = GetComponent<Rigidbody>();

        if (GameManager.Instance.Difficulty == 0)
        {
            speed = 5f;
        }
        else if (GameManager.Instance.Difficulty == 1)
        {
            speed = 10f;
        }
        else if (GameManager.Instance.Difficulty == 2)
        {
            speed = 15f;
        }
    }


    private void Start()
    {
        direction = new Vector3(Random.value > 0.5f ? 1 : -1, Random.Range(-1f, 1f), 0f).normalized;
        ball.velocity = direction * speed;
    }


    private void OnEnable()
    {
        Start();
    }


    private void OnDisable()
    {
        ball.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerLeft")
        {
            float directionY = HitFactor(transform.position, other.gameObject.transform.position, other.gameObject.transform.localScale.y);
            Vector3 direction = new Vector3(1f, directionY, 0f).normalized;
            ball.velocity = direction * speed;
        }
        else if (other.gameObject.tag == "PlayerRight")
        {
            float directionY = HitFactor(transform.position, other.gameObject.transform.position, other.gameObject.transform.localScale.y);
            Vector3 direction = new Vector3(-1f, directionY, 0f).normalized;
            ball.velocity = direction * speed;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WallVertical")
        {
            if (collision.gameObject.name == "WallLeft")
            {
                LevelManager.Instance.SetRoundWinner(2);
            }
            else if (collision.gameObject.name == "WallRight")
            {
                LevelManager.Instance.SetRoundWinner(1);
            }
        }
    }


    private float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }


    public void StopBall()
    {
        direction = ball.velocity.normalized;
        ball.velocity = Vector3.zero;
    }


    public void ResumeBall()
    {
        ball.velocity = direction * speed;
    }
}
