using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterControl : MonoBehaviour
{
    private Animator animator;
    public Rigidbody rb;
    public Transform cameraTransform;
    // Start is called before the first frame update

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    private Vector3 currentVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentVelocity = new(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacter();
        rotateCharacter();
    }


    private void rotateCharacter()
    {
        if(currentVelocity.sqrMagnitude > 0.1f)
        {
            rb.transform.LookAt(transform.position + currentVelocity);

        }
    }

    private void moveCharacter()
    {
        //to add together the sideways movements, have a direction vector where i just add to it
        //the fall guy should turn into this direction with soome sort of interpolation, but he immediately moves there???
        Vector3 movementDirection = new Vector3(0, 0, 0);
        Vector3 fwdMovement = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        Vector3 rightMovement = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z);

        if (Input.GetKey("w") && !Input.GetKey("s"))
        {
            //print("move forward");
            movementDirection += fwdMovement * maxSpeed;

        }
        else if (Input.GetKey("s") && !Input.GetKey("w"))
        {
            //print("move back");
            movementDirection -= fwdMovement * maxSpeed;
        }

        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            movementDirection -= rightMovement * maxSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            movementDirection += rightMovement * maxSpeed;
            //print("move right");
        }


        currentVelocity = movementDirection * maxSpeed;
        rb.MovePosition(rb.position + currentVelocity * Time.deltaTime);



        //currentVelocity += acceleration * Time.deltaTime * movementDirection;
        /*
        if(currentVelocity.x > maxSpeed)
        {
            currentVelocity.x = maxSpeed;
        } 
        else if (currentVelocity.x < -maxSpeed)
        {
            currentVelocity.x = -maxSpeed;
        }

        if (currentVelocity.y > maxSpeed)
        {
            currentVelocity.y = maxSpeed;
        }
        else if (currentVelocity.y < -maxSpeed)
        {
            currentVelocity.y = -maxSpeed;
        }

        if (currentVelocity.z > maxSpeed)
        {
            currentVelocity.z = maxSpeed;
        }
        else if (currentVelocity.z < -maxSpeed)
        {
            currentVelocity.z = -maxSpeed;
        }*/
    }
}
