using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Public allows it to be edited in inspector
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    // Needed in order to properly utilize Casting
    public ContactFilter2D movementFilter;
    public Vector2 movementInput;
    public float lastHorzInput;
    public float lastVertInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    // Creates an empty list, Raycasting utilizes this list to find collisions
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator objectAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Finds rigidbody2D in the object the script is attached to
        rb = GetComponent<Rigidbody2D>();
        objectAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate is used for in game physics 
    private void FixedUpdate()
    {
        // If movement input is anything other than zero, you can move
        if(movementInput != Vector2.zero){
            bool result = AbleToMove(movementInput);

            if(result != true){
                result = AbleToMove(new Vector2(movementInput.x, 0));
            }

            if(result != true){
                result = AbleToMove(new Vector2(0, movementInput.y));
            }   

            objectAnimator.SetBool("isMoving", result);
        }
        else {
            objectAnimator.SetBool("isMoving", false);
        }

        // Will flip the sprite according to the direction of movement
        if(movementInput.x < 0){
            spriteRenderer.flipX = false;
        }
        else if(movementInput.x > 0){
            spriteRenderer.flipX = true;
        }

        // This controls the weapons in HideWeapon script
        // The appearance of the weapons depends on movement direction
        if(movementInput.x != 0){
            lastHorzInput = movementInput.x; 
        }
        if(movementInput.y != 0){
            lastVertInput = movementInput.y;
        }

    }
    private bool AbleToMove(Vector2 direction)
    {
        if(direction != Vector2.zero){
        // Using Cast to check for collision with the player and the environment
        int counter = rb.Cast(
            direction,  //Input of what direction the player is moving
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

            // Using fixedDeltaTime because of FixedUpdate
            if(counter == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

                return true;
            }

            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();        
    }

    
}
