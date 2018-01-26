using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BatController : MonoBehaviour
{

    private Rigidbody2D rb;

    public float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!BatRayController.isFiringRay)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 horizontal = transform.right * x;
        Vector2 vertical = transform.up * y;

        Vector2 velocity = (horizontal + vertical).normalized * (Time.deltaTime * moveSpeed);

        Vector2 currentPosition = new Vector3(transform.position.x, transform.position.y);

        rb.MovePosition(currentPosition + velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {

        }
    }
}
