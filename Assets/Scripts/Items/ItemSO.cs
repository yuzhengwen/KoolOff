using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item SO")]
public class ItemSO : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string abilityName;
}
