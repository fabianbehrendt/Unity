using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float speed = 4f;
    private float jumpStrength = 20f;
    private float inputMovement = 0f;
    private float inputJump = 0f;
    private int jumpCount = 3;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        inputMovement = Input.GetAxisRaw("Horizontal") * speed;
        inputJump = Input.GetAxisRaw("Jump") * jumpStrength;

        Debug.Log(jumpCount);
    }


    private void FixedUpdate()
    {
        Vector3 forceMovement = new Vector3(inputMovement * speed * Time.deltaTime, 0f, 0f);

        playerRigidbody.MovePosition(transform.position + forceMovement);

        if (jumpCount <= 2 && inputJump != 0f)
        {
            Vector3 forceJump = new Vector3(0f, inputJump * jumpStrength * Time.deltaTime, 0f);
            playerRigidbody.AddForce(forceJump, ForceMode.Impulse);
            jumpCount++;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Platform")
        {
            jumpCount = 0;
        }
    }
}
