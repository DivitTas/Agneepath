using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public AudioSource deathSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage, remaining health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathSound != null) { deathSound.Play(); }
        Destroy(gameObject, 0.75f);
    }
}
