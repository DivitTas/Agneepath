using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private InputSystem_Actions action;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        action.Enable();
        action.Player.Attack.performed += OnAttack;
    }
    void OnDisable()
    {
        action.Disable();
        action.Player.Attack.performed -= OnAttack;
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack! RAWR!");
    }

    // Update is called once per frame
    void Update()
    {
        //meow
    }
}
