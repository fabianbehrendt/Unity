using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    private float speed = 5f;
    private float input;
    private Vector3 initialPosition;


    private void Start()
    {
        player = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }


    private void OnDisable()
    {
        transform.position = initialPosition;
        player.velocity = Vector3.zero;
    }


    private void Update()
    {
        input = (gameObject.tag == "PlayerLeft") ? Input.GetAxisRaw("Vertical2") : Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        player.velocity = Vector3.up * input * speed;
    }
}
