using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform attackTarget;
    [SerializeField] float speed;
    [SerializeField] int healthPoints = 25;
    [SerializeField] int attackDamage = 20;
    GameObject targetGameObject;
    Player targetPlayer;
    Rigidbody2D rb;
    Animator animator;
    float timer = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPlayer = attackTarget.gameObject.GetComponent<Player>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    
    }

    private void FixedUpdate()
    {
        Vector2 direction = (attackTarget.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>()){
            Attack();
        }
    }


    private void Attack()
    {
        targetPlayer.TakeDamage(attackDamage);
    }

    public void TakeDamage(int damageFromPlayer)
    {
        healthPoints -= damageFromPlayer;
        timer -= Time.deltaTime;

        if(healthPoints < 1){
            animator.SetTrigger("Defeated");

            if(timer < 0){
                RemoveEnemy();
            }

        }

    }

    //Displays score on the gameplay screen

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

}
