using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Reference to the fire point position
    public float bulletSpeed = 10f; // Speed of the bullet
    public SpriteRenderer gunNotFireSprite; // Reference to the "gun not fire" sprite
    public SpriteRenderer gunFireSprite; // Reference to the "gunfire" sprite
    [SerializeField] private float fireDuration = 0.2f; // Duration to show the "gunfire" sprite

        public void Start()
    {
        // Make "gunfire" sprite invisible at the beginning
        gunFireSprite.enabled = false;
    }
    void Update()
    {
        // Fire the gun when the player clicks the left mouse button
        if (Input.GetButtonDown("Fire1")) // Default is left mouse button
        {
            if (NewPlayer.Instance.maxAmmo > 0)
            {
                Shoot();
                Debug.Log("fired Ammo:");
            }
            else
            {
                Debug.Log("you have no ammo. not fired");
            }
        }
    }

    void Shoot()
    {
        // Create a bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Add velocity to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * bulletSpeed; // Adjust direction as needed
        }

        NewPlayer.Instance.ammoUsed(); // Decrease the player's ammo count

        // Toggle the sprites
        StartCoroutine(ToggleGunSprites());
    }

    IEnumerator ToggleGunSprites()
    {
        // Make "gun not fire" sprite invisible and "gunfire" sprite visible
        gunNotFireSprite.enabled = false;
        gunFireSprite.enabled = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(fireDuration);

        // Revert the sprites
        gunNotFireSprite.enabled = true;
        gunFireSprite.enabled = false;
    }
}