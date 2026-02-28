using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private InputSystem_Actions action;
    public Rigidbody2D rb;

    public float attackCooldown = 0.5f;
    private float attackCooldownLeft = 0f;

    public Transform attackPoint;
    public Vector2 attackSize = new Vector2(1f, 0.5f);
    public LayerMask enemyLayer;
    public int attackDamage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
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
        if (attackCooldownLeft > 0f) return;
        attackCooldownLeft = attackCooldown;
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0f, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            //enemy take damage here hehe 
            Debug.Log("hit " + hit.name);
        }
        Debug.Log("we dangerous cuh");
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCooldownLeft > 0)
        {
            attackCooldownLeft -= Time.deltaTime;
        }
    }
}
