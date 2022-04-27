using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private LayerMask simpleEnemy;

    private BoxCollider2D box;
    private Rigidbody2D body;
    public Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        Debug.Log("The Player has taken " + damageTaken + " damage.");
        healthbar.SetHealth(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "simpleEnemy")
        {
            System.Random rd = new System.Random();
            TakeDamage(rd.Next(3, 7));
            if (transform.localScale == new Vector3(-1, 1, 1))
            {
                body.velocity = new Vector2(-1, 1);
            }
            else
            {
                body.velocity = new Vector2(1, 1);
            }
        }
        else if (collision.gameObject.tag == "bigEnemy")
        {
            System.Random rd = new System.Random();
            TakeDamage(rd.Next(7, 13));
            if (transform.localScale == new Vector3(-1, 1, 1))
            {
                body.velocity = new Vector2(-1, 1);
            }
            else
            {
                body.velocity = new Vector2(1, 1);
            }
        }

    }
}
