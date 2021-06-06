﻿using UnityEngine;

public enum ItemType { Arms, Outfit, Consumible };

public abstract class Item : ScriptableObject
{
    [Header("Item General")]
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
    public int maxStack = 1;
    public int level = 1;
    public float weight;
    public int price;
    
    public abstract ItemType GetItemType();

    public virtual string ItemToString() { return "Name: " + itemName + "\nWeight: " + weight + "\nPrice: " + price; ; }
}