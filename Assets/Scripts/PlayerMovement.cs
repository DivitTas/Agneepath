using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public InputSystem_Actions action;
    public Rigidbody2D rb;

    private void OnEnable()
    {
        action = new InputSystem_Actions();
        action.Enable();
        action.Player.Jump.performed += ctx => Jump();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        action.Disable();
        action.Player.Jump.performed -= ctx => Jump();
    }

    private void Jump()
    {
        throw new NotImplementedException();
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
        rb.linearVelocity += input;
        Debug.Log(transform.position);
    }
}
