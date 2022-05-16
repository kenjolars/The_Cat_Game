 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour
{
    public float speed = 10;
    private Vector3 moveDir;
    private Rigidbody rb;
    public float jumpForce = 1000;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * jumpForce);
        }*/

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * speed * Time.deltaTime);
    }
}
