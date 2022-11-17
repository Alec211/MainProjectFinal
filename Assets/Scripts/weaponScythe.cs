using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScythe : MonoBehaviour
{
    [SerializeField] GameObject leftScytheObject;
    [SerializeField] GameObject rightScytheObject;
    [SerializeField] Vector2 weaponSize = new Vector2(4f, 4f);
    [SerializeField] float timeToAttack = 3f;
    [SerializeField] int scytheDamage = 13;
    PlayerController playerController;
    Animator animator;
    private float timer;

    private Collider2D[] potentialColliders;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();    
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f){
            Attack();
        }

    }

    private void Attack()
    {
        timer = timeToAttack;

        if(playerController.lastHorzInput > 0){
            // Enables the object and briefly shows it on screen
            rightScytheObject.SetActive(true);
            // Using overlapbox function because we wants to change the children not the parent
            // It detects any object in said box, only interact with enemies and destructables
            // Very similar to the method used in raycasting we can use an array for all potential colliders
            potentialColliders = Physics2D.OverlapBoxAll(rightScytheObject.transform.position, weaponSize, 0f);
            DamageDealt(potentialColliders);
        }
        else{
            // Enables the object and briefly shows it on screen
            leftScytheObject.SetActive(true);
            potentialColliders = Physics2D.OverlapBoxAll(leftScytheObject.transform.position, weaponSize, 0f);
            DamageDealt(potentialColliders);
        }

    }

    private void DamageDealt(Collider2D[] colliders)
    {
        for(int i = 0; i < colliders.Length; i++){
            //Debug.Log(colliders[i].gameObject.name);
            Enemy enemy = colliders[i].GetComponent<Enemy>();
            if(enemy != null){
                colliders[i].GetComponent<Enemy>().TakeDamage(scytheDamage);
            }            
        }
    }


}
