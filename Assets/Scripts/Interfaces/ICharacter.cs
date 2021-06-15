using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void Hit();
    void ReceiveHit(int damage);
    void Dropeable();
}
