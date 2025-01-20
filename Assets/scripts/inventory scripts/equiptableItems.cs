using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class EquiptableItems : ScriptableObject
{
    [Header("Only Gameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4); // Corrected typo

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
}

public enum ItemType
{
    Weapon,
    Tool,
    Consume
}

public enum ActionType
{
    attackMelee,
    attackRange,
    
    consumeHeal,
    consumeAttackUp,
    consumespeedUp,

}
