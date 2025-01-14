using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using UnityEngine.UI; //for ui namespace such as image.

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpHeight = 10;

    //coin variables
    [SerializeField] private TMP_Text CoinsText; // Reference to the TextMeshPro UI component
    public int coinCollected;  //Counter for amount of coins collected

    //health variables.
    public int health = 100;
    private int maxHealth = 300;
    public Image healthBar;
    [SerializeField] private Vector2 defaultHeathBarSize; //default size is 500. max = 1000.
    

    

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
        


        //update the healthbarUI
        healthBar.rectTransform.sizeDelta = new Vector2(defaultHeathBarSize.x * ((float)health / (float)maxHealth), healthBar.rectTransform.sizeDelta.y);
    }

    // This method could be called from other scripts to increase the player coins
    public void IncreaseCoinsCollected()
    {
        coinCollected++;  // Increase the coin count
        UpdateUI();   // Update the UI with new coin coint.
    }
}
