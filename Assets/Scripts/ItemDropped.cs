using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public Material mat;
    public Material matLife;
    public Material matMana;

    public int idItem = -1;
    public int amount = 1;

    void Start()
    {
        idItem = (idItem < 0) ? GameplayManager.GetInstance().GetRandomItemID() : idItem;
        amount = GameplayManager.GetInstance().GetRandomAmmountOfItem(idItem);

        Debug.Log("ID instanciado: " + idItem);

        MeshFilter meshFil = GetComponent<MeshFilter>();
        MeshRenderer meshRen = GetComponent<MeshRenderer>();
        MeshCollider meshCol = GetComponent<MeshCollider>();

        meshFil.mesh = GameplayManager.GetInstance().GetItemFromID(idItem).mesh;
        meshCol.sharedMesh = meshFil.mesh;
        if (GameplayManager.GetInstance().GetItemFromID(idItem).GetItemType() == ItemType.Consumible)
        {
            if (GameplayManager.GetInstance().GetItemFromID(idItem).itemName == "Life Potion")
            {
                meshRen.material = matLife;
            }
            else
            {
                meshRen.material = matMana;
            }
        }
        else
        {
            meshRen.material = mat;
        }
    }

    void Update()
    {
        
    }
}
