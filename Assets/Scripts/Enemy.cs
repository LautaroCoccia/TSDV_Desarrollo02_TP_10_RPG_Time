using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    public Stats stats;
    public GameObject itemDrop;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Dropeable();
    }

    public void Hit()
    {
        throw new NotImplementedException();
    }

    public void ReceiveHit(int damage)
    {
        throw new NotImplementedException();
    }

    public void Dropeable()
    {
        throw new NotImplementedException();
    }
}