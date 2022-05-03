using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    //Variables
    public float speed = 10f;
    public float gravity = 9.81f;
    public float jump = 4;

    private CharacterController controller1;

    private float directionY;

    // Start is called before the first frame update
    void Start()
    {
        controller1 = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();

        if(controller1.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                directionY = jump;
                Debug.Log("Space pressed");
            }
        }

        directionY -= gravity * Time.deltaTime;

        direction.y = directionY;

        controller1.Move(direction * speed * Time.deltaTime);
    }
}
