using UnityEngine;
using System;
public class Meteorite : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        ICharacter checkICharacter = other.gameObject.GetComponent<ICharacter>();

        if (checkICharacter != null)
        {
            checkICharacter.ReceiveHit(999);
        }
    }
}
