using System;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Chase,
        Dead
    }

    private State currentState;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Detection")]
    public float detectionRadius = 5f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Transform player;
    private bool movingRight = true;

    [Header("Attack")]
    public float attackCooldown = 1f;
    private float attackCooldownLeft = 0f;

    private bool playerInRange = false;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        currentState = State.Patrol;
        playerLayer = LayerMask.GetMask("Player");
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                DetectPlayer();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Dead:
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }
    private void Update()
    {
        if (attackCooldownLeft > 0)
            attackCooldownLeft -= Time.deltaTime;

        if (playerInRange && attackCooldownLeft <= 0f)
        {
            Attack();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        Debug.Log("Something entered: " + other.name);

        if (ph != null)
        {
            playerInRange = true;
            playerHealth = ph;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();

        if (ph != null)
        {
            playerInRange = false;
            playerHealth = null;
        }
    }

    private void Attack()
    {
        if (playerHealth == null)
            return;
        animator.SetTrigger("Attack");
        playerHealth.TakeDamage(1);
        attackCooldownLeft = attackCooldown;
    }

    void Patrol()
    {
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);


        if (movingRight && rb.position.x >= rightPoint.position.x)
            movingRight = false;

        if (!movingRight && rb.position.x <= leftPoint.position.x)
            movingRight = true;
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(rb.position, detectionRadius, playerLayer);

        if (hit != null)
        {
            player = hit.transform;
            currentState = State.Chase;
        }
    }

    void Chase()
    {
        if (player == null)
        {
            currentState = State.Patrol;
            return;
        }

        float direction = player.position.x > rb.position.x ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

    }


}