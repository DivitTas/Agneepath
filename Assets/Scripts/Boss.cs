using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int health = 3;
    public AudioSource deathSound;
    public string gameOverSceneName = "GameOver";
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathSound = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.x) >= 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else if (rb.linearVelocity.x == 0) 
        { 
            animator.SetBool("IsWalking", false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage, remaining health: " + health);
        animator.SetTrigger("Hit");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathSound != null) { deathSound.Play(); }
        BossAI ai = GetComponent<BossAI>();
        if (ai != null)
        {
            ai.enabled = false;
        }
        animator.SetTrigger("Die");
        Invoke("LoadGameOver", 1.75f);
        Destroy(gameObject, 1.75f);
    }

    void LoadGameOver()
    {
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
