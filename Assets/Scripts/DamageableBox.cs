using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBox : MonoBehaviour
{
    public GameObject objetive;

    private void OnEnable()
    {
        objetive = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        ICharacter checkICharacter = other.gameObject.GetComponent<ICharacter>();

        if (checkICharacter != null)
        {
            objetive = other.gameObject;
        }
        gameObject.SetActive(false);
    }
}
