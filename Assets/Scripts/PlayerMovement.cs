using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    public InputSystem_Actions action;
    public Rigidbody2D rb;
    public float speedMultiplier = 9f;
    public float acceleration = 40f;
    public float deceleration = 200;
    public float jumpForce = 20f;
    private void OnEnable()
    {
        action = new InputSystem_Actions();
        action.Enable();
        action.Player.Jump.performed += OnJump;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnDisable()
    {
        action.Disable();
        action.Player.Jump.performed -= OnJump;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 HandleInput()
    {
        return action.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 input = HandleInput();
        float targetSpeed = input.x * speedMultiplier;
        float currentSpeed = rb.linearVelocity.x;
        float accelRate;
        // later implement logic to round speed to 9 if greater than 7.5 for controller stick drift adjustment
        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            accelRate = deceleration;
        }
        else if (targetSpeed*currentSpeed<0)
        {
            accelRate = deceleration;
        }
        else
        {
            accelRate = acceleration;
        }
        float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelRate * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newSpeed, rb.linearVelocity.y);
        Debug.Log(rb.linearVelocity);
    }
}
