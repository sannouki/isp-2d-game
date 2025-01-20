using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private Vector2 raycastoffset;
    [SerializeField] private float raycastlength = 2f;
    private int direction = 1;
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightwallRaycastHit;
    private RaycastHit2D leftwallRaycastHit;
    public int health = 100;
    private int maxhealth = 100;
    [SerializeField] private LayerMask rayCastLayerMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for right ledge!
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down, raycastlength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x + raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down * raycastlength, Color.blue);
        if (rightLedgeRaycastHit.collider == null)
        {
            Debug.Log("Right ledge detected, turning left");
            direction = -1;
        }
        //Check for left ledge!
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down, raycastlength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x - raycastoffset.x, transform.position.y + raycastoffset.y), Vector2.down * raycastlength, Color.green);
        if (leftLedgeRaycastHit.collider == null)
        {
            Debug.Log("Left ledge detected, turning right");
            direction = 1;
        }
        //Check for right wall!
        rightwallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastlength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * raycastlength, Color.red);
        if (rightwallRaycastHit.collider != null)
        {
            Debug.Log("Right wall detected, turning left");
            direction = -1;
        }

        //Check for left wall!
        leftwallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastlength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * raycastlength, Color.magenta);
        if (leftwallRaycastHit.collider != null)
        {
            Debug.Log("Left wall detected, turning right");
            direction = 1;
        }

        //if health <0 destroy me (set gameobject to false. look at coins for true deleting object)
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    //If I collide with the player, hurt the player (health is going to decrease, update the UI)
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            Debug.Log("Hurt the player!");
            NewPlayer.Instance.health -= 10;
            NewPlayer.Instance.UpdateUI();
        }
    }
}
