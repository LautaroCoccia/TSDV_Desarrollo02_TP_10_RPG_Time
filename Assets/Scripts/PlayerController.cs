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
    public GameObject damageable;
    float time = 0;

    public Stats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = player.GetComponent<Animator>();
        inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        time += Time.deltaTime;
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
        else if (!inventoryOpened && Input.GetMouseButtonDown(0)) // Attack
        {
            Debug.Log("ataque");
            if (time > stats.atkRate)
            {
                time = 0;
                int atk = Random.Range(1, 6);
                anim.SetInteger("AttackStyle", atk);
                anim.SetTrigger("Attack");
                damageable.SetActive(true);
                Invoke(nameof(Hit), 0.1f);
            }
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
        GameObject objetive = damageable.GetComponent<DamageableBox>().objetive;
        if (objetive == null)
            return;

        objetive.GetComponent<ICharacter>().ReceiveHit(stats.damage);
    }

    public void ReceiveHit(int damage)
    {
        stats.health -= damage;
        if (stats.health < 1)
        {
            stats.health = 0;
            anim.SetBool("Die", true);
        }   
        else
        {   
            anim.SetTrigger("TakeDamage");
        }   
        LevelManager.Get().UpdateHealth(stats.health);
    }

    public void Dropeable()
    {
        throw new System.NotImplementedException();
    }
}