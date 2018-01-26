using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour {
    public float horizontal;
    public float vert;
    public float hor;
    public float turnSpeed;
    public float walkSpeed;
    public float currentSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpHeight;
    public Animator anim;
    public Rigidbody player;


	void Start () {
        currentSpeed = walkSpeed;
        player = GetComponent<Rigidbody>();
	}
	

	void Update () {
        Movement();
    }

    public void Movement()
    {
        horizontal = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        vert = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
        hor = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(0, horizontal, 0);
        }
        if (Input.GetButton("Horizontal"))
        {
            transform.Rotate(0, hor, 0);
        }
        if(Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            vert = 1 * currentSpeed * Time.deltaTime; ;
        }
        transform.Translate(0, 0, vert);

        if (Input.GetButton("Crouch"))
        {
            Crouch();
        }
        if (Input.GetButtonUp("Crouch"))
        {
            currentSpeed = walkSpeed;
            anim.SetBool("Crouch", false);
        }
        if (Input.GetButton("Run"))
        {
            Run();
        }
        if (Input.GetButtonUp("Run"))
        {
            currentSpeed = walkSpeed;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    public void Crouch()
    {
        anim.SetBool("Crouch", true);
        currentSpeed = crouchSpeed;
    }

    public void Jump()
    {
        Vector3 fwd = transform.TransformDirection(-Vector3.up);
        if (Physics.Raycast(transform.position, fwd, 1.1f))
        {

            player.AddForce(transform.up * jumpHeight);
        }
    }

    public void Run()
    {
        currentSpeed = runSpeed;
    }
}
