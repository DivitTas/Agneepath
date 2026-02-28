using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    public InputSystem_Actions action;
    public Rigidbody2D rb;
    public int facingDirection = 1; // 1 = right, -1 = left

    public float speedMultiplier = 9f;
    public float acceleration = 40f;
    public float deceleration = 200;

    public float jumpForce = 20f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownLeft;
    private float originalGravity;

    private void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        action.Enable();
        action.Player.Jump.performed += OnJump;
        action.Player.Dash.performed += OnDash;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        TryDash();
    }
    private void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownLeft = dashCooldown;
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
    }
    private void EndDash()
    {
        isDashing = false;
        rb.gravityScale = originalGravity;
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        if(IsGrounded()) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnDisable()
    {
        action.Disable();
        action.Player.Jump.performed -= OnJump;
        action.Player.Dash.performed -= OnDash;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //nothign yet ;p
        if (dashCooldownLeft > 0f)
        {
        dashCooldownLeft -= Time.deltaTime;
        }
    }

    Vector2 HandleInput()
    {
        return action.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        
        Vector2 input = HandleInput();
        if (input.x > 0.01f)
            facingDirection = 1;
        else if (input.x < -0.01f)
            facingDirection = -1;
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0);
            dashTimeLeft -= Time.fixedDeltaTime;
            if(dashTimeLeft <= 0)
            {
                EndDash();
            }
            return;
        }
        float targetSpeed = input.x * speedMultiplier;
        float currentSpeed = rb.linearVelocity.x;
        float accelRate;
        // later implement logic to round speed to 9 if greater than 7.5 for controller stick drift adjustment
        if (Mathf.Abs(targetSpeed) < 0.01f)
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
        //Debug.Log(IsGrounded());
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void TryDash()
    {
        if(isDashing) return;
        if (dashCooldownLeft > 0) return;
        StartDash();
        
    }
}
