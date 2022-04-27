using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
    float walkdirection;
    bool jump;
    bool role;
    bool block;
    bool attack;

    [SerializeField] private float gravity;
    [SerializeField] private float walkspeed;
    [SerializeField] private float jumpheight;
    [SerializeField] private float attackRange;

    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attackPoint;
    private Rigidbody2D body;
    private BoxCollider2D box;
    private Animator anim;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //transform.rotation = new Quaternion(0, 0, 0, 0);
        GetMovement();
        FlipPlayer();
        MovePlayer();
        JumpPlayer();
        PlayerAttack();
        if (IsOnWall() && !IsGrounded())
        {
            body.gravityScale = gravity;
        }
        //anim.SetBool("run", walkdirection != 0);
    }

    private void GetMovement()
    {
        walkdirection = Input.GetAxis("Horizontal");
        jump = Input.GetKey("space");
        attack = Input.GetKeyDown(KeyCode.Mouse0);
    }

    private void FlipPlayer()
    {
        if (walkdirection > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (walkdirection < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void MovePlayer()
    {
        //transform.localScale = new Vector2(transform.position.x * walkspeed * walkdirection, transform.position.y);
        body.velocity = new Vector2(walkdirection * walkspeed, body.velocity.y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D isOnGround = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return isOnGround.collider != null;
    }

    private void JumpPlayer()
    {
        if (jump && IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpheight);
        }
    }

    private void PlayerAttack()
    {
        if (attack)
        {
            //Hier animation

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                System.Random rd = new System.Random();
                enemy.GetComponent<EnemyHealth>().TakeDamage(rd.Next(minDamage, maxDamage));
            }
        }
    }

    private bool IsOnWall()
    {
        RaycastHit2D isOnGround = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.01f, wallLayer);
        return isOnGround.collider != null;
    }
}
