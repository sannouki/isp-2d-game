using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f; // Destroy bullet after 5 seconds
    public int damage = 100; 

    void Start()
    {
        //Destroy(gameObject, lifetime); // Destroy bullet automatically
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Destroy bullet on collision
    }
}
