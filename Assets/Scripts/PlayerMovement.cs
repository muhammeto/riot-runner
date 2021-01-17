using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float deadZone = 0.1f;

    Vector3 movementDelta;
    private float mouseStart;
    private Rigidbody rb;
    private Animator animator;
    private bool isDead = false;
    private bool isFinish = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (isDead || isFinish) return;
        movementDelta = Vector3.forward * forwardSpeed;
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Input.mousePosition.x;
        }else if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - mouseStart;
            mouseStart = Input.mousePosition.x;
            if(Mathf.Abs(delta) <= deadZone)
            {
                delta = 0;
            }
            else
            {
                delta = Mathf.Sign(delta);
            }
            movementDelta += Vector3.right * horizontalSpeed * delta;
        }
    }

    private void FixedUpdate()
    {
        if (isDead || isFinish) return;
        rb.MovePosition(rb.position + movementDelta * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Dead
            animator.SetTrigger("Death");
            isDead = true;
        }else if (collision.gameObject.CompareTag("Finish"))
        {
            animator.SetTrigger("Victory");
            isFinish = true;
        }
    }
}
