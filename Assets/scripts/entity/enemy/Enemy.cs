using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private Vector2 raycastoffset;
    [SerializeField] private float raycastlength = 2f;
    [SerializeField] private Vector2 wallRaycastOffset;
    [SerializeField] private float wallRaycastLength = 2f;
    private int direction = 1;
    private RaycastHit2D rightLedge;
    private RaycastHit2D leftLedge;
    private RaycastHit2D rightwall;
    private RaycastHit2D leftwall;
    public int health = 100;
    [SerializeField] private LayerMask rayCastLayerMask;

    private bool spriteFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        //start the enemy facing right
        spriteFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //checking for ledges 
        //right ledge
        rightLedge = Physics2D.Raycast(new Vector2(transform.position.x + raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down, raycastlength);
        Debug.DrawRay(new Vector2(transform.position.x + raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down * raycastlength, Color.blue);
        if (rightLedge.collider == null)
        {
            Debug.Log("Right ledge detected, turning left");
            direction = -1;
            spriteFacingRight = false;
            FlipSprite();
        }
        else
        {
            Debug.Log(rightLedge.collider.gameObject);
        }

        //left ledge
        leftLedge = Physics2D.Raycast(new Vector2(transform.position.x - raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down, raycastlength);
        Debug.DrawRay(new Vector2(transform.position.x - raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down * raycastlength, Color.green);
        if (leftLedge.collider == null)
        {
            Debug.Log("Left ledge detected, turning right");
            direction = 1;
            spriteFacingRight = true;
            FlipSprite();
        }
        else
        {
            Debug.Log(leftLedge.collider.gameObject);
        }

        //left wall
        leftwall = Physics2D.Raycast(new Vector2(transform.position.x + wallRaycastOffset.x, transform.position.y + wallRaycastOffset.y), Vector2.left, wallRaycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + wallRaycastOffset.x, transform.position.y + wallRaycastOffset.y), Vector2.left * wallRaycastLength, Color.magenta);
        if (leftwall.collider != null)
        {
            Debug.Log("Left wall detected, turning right");
            direction = 1;
            spriteFacingRight = true;
            FlipSprite();
        }


        //checking for walls
        //right wall
        rightwall = Physics2D.Raycast(new Vector2(transform.position.x + wallRaycastOffset.x, transform.position.y + wallRaycastOffset.y), Vector2.right, wallRaycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + wallRaycastOffset.x, transform.position.y + wallRaycastOffset.y), Vector2.right * wallRaycastLength, Color.red);
        if (rightwall.collider != null)
        {
            Debug.Log("Right wall detected, turning left");
            direction = -1;
            spriteFacingRight = false;
            FlipSprite();
        }
    }

    //If I collide with the player, hurt the player (health is going to decrease, update the UI)
    /* private void OnCollisionEnter2D(Collision2D col)
     {
         if (col.gameObject == NewPlayer.Instance.gameObject)
         {
             Debug.Log("Hurt the player!");
             NewPlayer.Instance.health -= 10;
             NewPlayer.Instance.UpdateUI();
         }
     }

     private void OnCollisionEnter2D(Collision2D collision) { 
         // Check if the colliding object has the Bullet script attached
         Bullet bullet = collision.gameObject.GetComponent<Bullet>();
         if (bullet != null)
         {
             // Reduce enemy health by the bullet's damage
             health -= bullet.damage;
             Debug.Log($"Enemy took {bullet.damage} damage. Remaining health: {health}");

             // Destroy the bullet after it hits the enemy
             Destroy(collision.gameObject);

             // If health is less than or equal to 0, destroy the enemy
             if (health <= 0)
             {
                 Die();
             }
         }
     }
    */
    private void Die()
     {
         Debug.Log("Enemy is dead");
         Destroy(gameObject); // Destroy the enemy GameObject
     }

     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            Debug.Log("Hurt the player!");
            NewPlayer.Instance.health -= 10;
            NewPlayer.Instance.UpdateUI();
            return; // Exit after handling the player collision
        }

        // Check if the collision is with a bullet
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            // Reduce enemy health by the bullet's damage
            health -= bullet.damage;
            Debug.Log("Enemy took damage");

            // If health is 0 or below, enemy dies 
            if (health <= 0)
            {
                Die();
            }
        }
    }


    void FlipSprite()
    {
        if ((spriteFacingRight && transform.localScale.x < 0) || (!spriteFacingRight && transform.localScale.x > 0))
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}


