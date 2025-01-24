using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePointLeft; // Reference to the fire point position
    public Transform firePointRight; // Reference to the fire point position

    public SpriteRenderer gunNotFireSprite; // Reference to the "gun not fire" sprite

    public SpriteRenderer gunFireSpriteRight; // Reference to the "gunfire" sprite
    public SpriteRenderer gunFireSpriteLeft; // Reference to the "gunfire" sprite
    // References to the gun sprites for directional shooting
    public SpriteRenderer gunRightSprite;
    public SpriteRenderer gunLeftSprite;

    [Header("Gun Settings")]
    [SerializeField] public float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] private float fireDuration = 0.2f; // Duration to show the "gunfire" sprite
    [SerializeField] private float fireRate = 0.5f; // Rate of fire (time in seconds between shots)



    private float lastShootTime = 0f; // Tracks when the player last shot

    public void Start()
    {
        // Make "gunfire" sprite invisible at the beginning
        gunFireSpriteRight.enabled = false;
        gunFireSpriteLeft.enabled = false;
        lastShootTime = 0f;
    }

    void Update()
    {
        // Fire the gun when the player clicks the left mouse button, and only if enough time has passed since the last shot
        if (Input.GetButtonDown("Fire1") && Time.time - lastShootTime >= fireRate) // Default is left mouse button
        {
            if (NewPlayer.Instance.maxAmmo > 0)
            {
                Shoot();
                Debug.Log("fired Ammo:");
                lastShootTime = Time.time; // Update the last shot time only after firing
            }
            else
            {
                Debug.Log("you have no ammo. not fired");
            }
        }

        // Update gun sprite based on player's facing direction
        if (NewPlayer.Instance.IsFacingRight)
        {
            gunRightSprite.enabled = true;
            gunLeftSprite.enabled = false;
        }
        else
        {
            gunRightSprite.enabled = false;
            gunLeftSprite.enabled = true;
        }
    }

    void Shoot()
    {
        //shoot the bullet
        if (NewPlayer.Instance.IsFacingRight)
        {
            // Create a bullet at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);

            // Add velocity to the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePointRight.right * bulletSpeed; // Adjust direction as needed
            }
        }
        else //player is facing left
        {
            // Create a bullet at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);

            // Add velocity to the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePointLeft.right * -bulletSpeed; // Adjust direction as needed
            }
        }

        // Decrease the player's ammo count after firing
        NewPlayer.Instance.ammoUsed(); // Decrease the player's ammo count

        // Toggle the sprites
        StartCoroutine(ToggleGunSprites());
    }

    IEnumerator ToggleGunSprites()
    {
        if (NewPlayer.Instance.IsFacingRight)
        {
            // Make "gun not fire" sprite invisible and "gunfire" sprite visible
            gunNotFireSprite.enabled = false;
            gunFireSpriteRight.enabled = true;

            // Wait for the specified duration
            yield return new WaitForSeconds(fireDuration);

            // Revert the sprites
            gunNotFireSprite.enabled = true;
            gunFireSpriteRight.enabled = false;
        }
        else
        {
            // Make "gun not fire" sprite invisible and "gunfire" sprite visible
            gunNotFireSprite.enabled = false;
            gunFireSpriteLeft.enabled = true;

            // Wait for the specified duration
            yield return new WaitForSeconds(fireDuration);

            // Revert the sprites
            gunNotFireSprite.enabled = true;
            gunFireSpriteLeft.enabled = false;
        }
        
    }
}
