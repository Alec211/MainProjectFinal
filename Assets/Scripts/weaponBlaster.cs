using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBlaster : MonoBehaviour
{
    [SerializeField] GameObject topBlastObject;
    [SerializeField] GameObject bottomBlastObject;
    [SerializeField] Vector2 weaponSize = new Vector2(4f, 4f);
    [SerializeField] float timeToAttack = 3f;
    [SerializeField] int blastDamage = 13;
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
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f){
            Attack();
        }

    }

    //Greg dev stuff
    private void Attack()
    {
        timer = timeToAttack;

        if(playerController.lastHorzInput > 0){
            // Enables the object and briefly shows it on screen
            topBlastObject.SetActive(true);
            // Using overlapbox function because we wants to change the children not the parent
            // It detects any object in said box, only interact with enemies and destructables
            // Very similar to the method used in raycasting we can use an array for all potential colliders
            potentialColliders = Physics2D.OverlapBoxAll(topBlastObject.transform.position, weaponSize, 0f);
            DamageDealt(potentialColliders);
        }
        else{
            // Enables the object and briefly shows it on screen
            bottomBlastObject.SetActive(true);
            potentialColliders = Physics2D.OverlapBoxAll(bottomBlastObject.transform.position, weaponSize, 0f);
            DamageDealt(potentialColliders);
        }

    }

    //Greg dev stuff
    private void DamageDealt(Collider2D[] colliders)
    {
        for(int i = 0; i < colliders.Length; i++){
            //Debug.Log(colliders[i].gameObject.name);
            Enemy enemy = colliders[i].GetComponent<Enemy>();
            if(enemy != null){
                colliders[i].GetComponent<Enemy>().TakeDamage(blastDamage);
            }            
        }
    }
}
