using UnityEngine;

public class EnemyAI : MonoBehaviour
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    void Patrol()
    {
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (movingRight && transform.position.x >= rightPoint.position.x)
            movingRight = false;

        if (!movingRight && transform.position.x <= leftPoint.position.x)
            movingRight = true;
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

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

        float direction = player.position.x > transform.position.x ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    
}