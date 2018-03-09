using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D myRigidBody2D;
    private static bool playerExists;
    public float moveSpeed;

    // Use this for initialization
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput != Vector2.zero)
        {
            myRigidBody2D.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        }
        else
        {
            myRigidBody2D.velocity = Vector2.zero;
        }

    }
}
