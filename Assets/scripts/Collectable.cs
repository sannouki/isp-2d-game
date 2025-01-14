using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Creates an ItemType enum (drop down)
    enum ItemType { 
        Coin, 
        Healthitem, 
        SoulSpirit, 
        SpiritItem } 
    //items for future considerations: consumable items, coins, health packages
    [SerializeField] private ItemType itemType;
    NewPlayer newPlayer;
    // Start is called before the first frame update
    void Start()
    {
        newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    // Generate a random number between 25 and 50 (inclusive)
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player is touching me, print "Collect" in the console
        if (collision.gameObject.name == "Player") {
            if (itemType == ItemType.Coin) {
                newPlayer.coinCollected += 1;
                Debug.Log("debug-Collectable: Coin");
            }
        //if health is collected, gain 100 up to total max health
        else if (itemType == ItemType.Healthitem) {
            
                //note: this is the default health value. the totalmaxhealth (300) can be obtain through other game mechanics.
                int healingRangeValue = Random.Range(25, 51); //picking a healing item can heal between 25-50.
                if (newPlayer.health < 200) { 
                newPlayer.health += healingRangeValue;
                Debug.Log("debug-Collectable: Health: " + healingRangeValue);
                    if(newPlayer.health > 200)
                    {
                        newPlayer.health = 200;
                    }
                }

        }

        /*else if (itemType == ItemType.SpiritItem)
        {
            Debug.Log("Collectable: Spirit Item");
        }
        else if (itemType == ItemType.SoulSpirit)
        {
            Debug.Log("Collectable: Soul Spirit");
        }
        */

        else {
            Debug.Log("unsorted inventory item!");
        }


    newPlayer.UpdateUI();
    Destroy(gameObject);
        }
    }
}