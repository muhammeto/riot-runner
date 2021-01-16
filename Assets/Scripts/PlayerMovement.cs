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
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
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
        rb.MovePosition(rb.position + movementDelta * Time.fixedDeltaTime);
    }
}
