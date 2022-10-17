using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterControl : MonoBehaviour
{
    private Animator animator;
    private bool wasGrounded;
    //public GameObject fallguy;
    public Renderer renderer;
    public Rigidbody rb;
    public Transform cameraTransform;
    public float simpleGravity;
    //public bool isGrounded;
    // Start is called before the first frame update

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    private Vector3 currentVelocity;
    public Vector3 startPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //renderer = GetComponent<Renderer>();
        currentVelocity = new(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacterHorizontal();
        RotateCharacter();
        MoveCharacterVertical();
    }


    private void RotateCharacter()
    {
        if(currentVelocity.sqrMagnitude > 0.1f)
        {
            rb.transform.LookAt(transform.position + currentVelocity);

        }
    }

    private void MoveCharacterHorizontal()
    {
        //to add together the sideways movements, have a direction vector where i just add to it
        //the fall guy should turn into this direction with soome sort of interpolation, but he immediately moves there???
        if(animator.GetBool("Grounded")  == true)
        {
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

            if (movementDirection.magnitude > 0.01 || movementDirection.magnitude < -0.01)
            {
                animator.SetFloat("Speed", 2);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
            currentVelocity = movementDirection * maxSpeed;
           //Debug.Log(currentVelocity);
            rb.transform.position = (rb.position + currentVelocity * Time.deltaTime);

        }



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

    private void MoveCharacterVertical()
    {
        RaycastHit hit;
        Vector3 sphereCastCenter = renderer.bounds.center;
        float radiusSphere = renderer.bounds.size.x/4;// / 2.0f; //halbe weite der bounding box
        float distanceSphere = renderer.bounds.size.y / 2;//halb weiter idk
        if (Physics.SphereCast(sphereCastCenter, radiusSphere, -transform.up, out hit, distanceSphere))
        {
            //Debug.Log("distance " + hit.distance);
            //versuchen auf die top von dem objekt zu platzieren + hit.collider.gameObject.GetComponent<Renderer>().bounds.extents.y    
            //Debug.Log(hit.collider.gameObject.GetComponent<Renderer>().bounds.size.y);
            //Debug.Log(hit.collider.bounds.);

            //nur nach oben wenn wir neuen bodenkontakt haben und vorher gefallen sind
            //if(rb.position.y < 0)
            //{
            //    rb.MovePosition(new Vector3(rb.position.x, 0, rb.position.z));
            //}
            animator.SetBool("Grounded", true);
            TipToePlatform tipToePlatform = hit.collider.GetComponent<TipToePlatform>();
            if (tipToePlatform != null)
            {
                if (tipToePlatform.state_ == TipToePlatform.State.Dead)
                {
                    CharFalls();
                    animator.SetBool("Grounded", false);//dieser spezialfall benötigt überschreiben von dem Grounded thing
                }
                tipToePlatform.CharacterTouches();
            }
            
            
        } else
        {
            CharFalls();
            //wasGrounded = false;
        }

        if(transform.position.y < -8)
        {
            ResetCharacter();
        }
    }

    private void CharFalls()
    {
        Vector3 moveDown = new(0, simpleGravity, 0);
        rb.MovePosition(rb.position + moveDown * Time.deltaTime);
        animator.SetBool("Grounded", false);
        Debug.Log("Charfalls");
    }

    private void ResetCharacter()
    {
        transform.position = startPos;
    }
}
