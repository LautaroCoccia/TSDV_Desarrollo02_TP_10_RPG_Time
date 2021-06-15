using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Stats
{
    public int health = 100;
    public int damage = 10;
    public float atkRate = 1.2f;
}

public class PlayerController : MonoBehaviour, ICharacter
{
    public Player player;
    public float forceSpeed = 20;
    private Inventory inventory;
    private Rigidbody rb;
    public GameObject panelInventory;
    private bool inventoryOpened;
    public GameObject pickeable;
    public List<ItemDropped> itemsPicking;
    private Animator anim;

    public Stats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = player.GetComponent<Animator>();
        inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (!inventoryOpened)
            Move();
        InputsPlayer();
    }

    void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(hor, 0, ver);
        rb.AddForce(direction.normalized * forceSpeed);

        transform.LookAt(transform.position + direction);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
            anim.SetBool("IsMooving", true);
        else
            anim.SetBool("IsMooving", false);
    }
     
    void InputsPlayer()
    {
        if (!inventoryOpened && Input.GetKeyDown(KeyCode.E))        // Pick Up
        {
            pickeable.SetActive(true);
            anim.SetTrigger("PickUp");
        }
        else if (Input.GetKeyDown(KeyCode.Q))                       // Abrir Inventario
        {
            inventoryOpened = !inventoryOpened;
            panelInventory.SetActive(inventoryOpened);
        }
        else if (!inventoryOpened && Input.GetMouseButtonDown(0))   // Attack
        {
            int atk = Random.Range(1, 6);
            anim.SetInteger("AttackStyle", atk);
            anim.SetTrigger("Attack");
        }
        else if (!inventoryOpened && Input.GetMouseButtonDown(1))   // Spawn de Meteoritos
        {

        }
    }

    public void IncorporateItems(ItemDropped item, GameObject objectItem)
    {
        if (inventory.AddNewItem(item.idItem, item.amount))
        {
            Debug.Log("Objeto: " + item.idItem + " Pickeado.");
            Destroy(objectItem);
        }
        else
        {
            Debug.Log("No hay mas espacio en el Inventario.");
        }
    }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveHit(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Dropeable()
    {
        throw new System.NotImplementedException();
    }
}