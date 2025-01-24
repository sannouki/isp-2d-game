using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyredo : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private Vector2 raycastoffset;
    [SerializeField] private float raycastlength = 2f;
    private int direction = 1;
    private RaycastHit2D rightLedge;
    private RaycastHit2D leftLedge;
    private RaycastHit2D rightwall;
    private RaycastHit2D leftwall;
    public int health = 100;

    [SerializeField] private LayerMask rayCastLayerMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //checking for ledges 
        //right ledge
        rightLedge = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 2);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.down * 2, Color.blue);


    }
}