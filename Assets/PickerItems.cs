using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerItems : MonoBehaviour
{
    public PlayerController playerC;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemDropped itemDrop = other.GetComponent<ItemDropped>();
            playerC.IncorporateItems(itemDrop, other.gameObject);
        }
        gameObject.SetActive(false);
    }
}