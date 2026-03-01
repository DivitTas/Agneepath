using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private InputSystem_Actions action;
    public Rigidbody2D rb;
    private PlayerMovement playerMovement;

    public float attackCooldown = 0.5f;
    private float attackCooldownLeft = 0f;

    public Transform attackPoint;
    public Vector2 attackSize = new Vector2(3.6f, 3.7f);
    public LayerMask enemyLayer;
    public int attackDamage = 1;
    
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("Attack");
        attackCooldownLeft = attackCooldown;
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0f, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            Debug.Log("hit " + hit.name);
            Enemy enemy = hit.GetComponent<Enemy>();
            Boss boss = hit.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
            else if(boss != null)
            {
                boss.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogWarning("Hit collider " + hit.name + " but no Enemy component found!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCooldownLeft > 0)
        {
            attackCooldownLeft -= Time.deltaTime;
        }
        Vector3 localPos = attackPoint.localPosition;
        localPos.x = Mathf.Abs(localPos.x) * playerMovement.facingDirection;
        attackPoint.localPosition = localPos;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackSize);
    }
}
