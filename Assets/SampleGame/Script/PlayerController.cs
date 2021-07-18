using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float speed = 3.0f;
    private float gravity = 9.8f;
    [SerializeField]
    private float m_jumpPower = 15f;
    bool m_isGrounded = false;
    Rigidbody m_rb = default;
    
   


    private void OnTriggerExit(Collider other)
    {
        m_isGrounded = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        m_isGrounded = true;
        
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        CalculateMove();

        if (Input.GetButtonDown("Jump"))
        {      


            if (m_isGrounded == true)
            {
                m_rb.velocity = Vector3.zero;
                m_rb.AddForce(Vector3.up * this.m_jumpPower, ForceMode.Impulse);
               
            }
            
        }
    }

    void CalculateMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, m_rb.velocity.y, verticalInput);
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);
        controller.Move(velocity * Time.deltaTime);
    }
}
