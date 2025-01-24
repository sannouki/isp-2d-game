using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using UnityEngine.UI; //for ui namespace such as image.

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpHeight = 10;
    //directions
    public bool IsFacingRight { get; private set; }

    //collectable variables
    [SerializeField] private TMP_Text CoinsText; // Reference to the TextMeshPro UI for coins
    public int coinCollected;  //Counter for amount of coins collected
    [SerializeField] private TMP_Text AmmoText; //Reference to the TextMeshPro UI for ammo
    public int maxAmmo;

    //health variables.
    public int health = 100;
    private int maxHealth = 150;
    public Image healthBar;
    [SerializeField] private Vector2 defaultHeathBarSize; //default size is 500. max = 1000.

    //dictionary for scriptable items

    //Singleton instantation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //starting health
        defaultHeathBarSize = healthBar.rectTransform.sizeDelta;

        //Coin
        // Find the TMP_Text label called "CoinsText" in scene1
        //CoinsText = GameObject.Find("CoinsText").GetComponent<TMP_Text>();
        
        
        
        // Update the UI on start
        UpdateUI();
    }

    // Update is called once per frame (used for movement and jumping)
    void Update()
    {
        //Handle player movement (left-right) based on input
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        // Check if the player is facing right or left
        facingDirection();

        // Handle jumping when the "Jump" button is pressed and the player is grounded
        if (Input.GetButtonDown("Jump") && grounded){
            velocity.y = jumpHeight;
        }



    }

    // Update the coin count displayed in the UI
    public void UpdateUI()
    {
        // Update coins
        CoinsText.SetText(coinCollected.ToString());

        // Update bullet count
        AmmoText.SetText(maxAmmo.ToString());

        //update the healthbarUI
        healthBar.rectTransform.sizeDelta = new Vector2(defaultHeathBarSize.x * ((float)health / (float)maxHealth), healthBar.rectTransform.sizeDelta.y);
    }




    // This method could be called from other scripts to increase the player coins
    public void IncreaseCoinsCollected()
    {
        coinCollected++;  // Increase the coin count
        UpdateUI();   // Update the UI with new coin coint.
    }

    // This method could be called from other scripts to decrease the ammo count
    public void ammoUsed()
    {
        maxAmmo--;  // decrease 1 bullet per use
        UpdateUI();   // Update the UI new ammo.
    }
    
    // This method control which direction the player is facing.
    public void facingDirection()
    {
        if (targetVelocity.x > 0){
            IsFacingRight = true;
            Debug.Log("Player facing: Right");
        }
        else if (targetVelocity.x < 0) {
            IsFacingRight = false;
            Debug.Log("Player facing: left");
        }
    }
}
