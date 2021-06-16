using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, ICharacter
{
    private Animator anim;
    private GameObject player;
    public GameObject damageable;
    public Stats stats;
    [SerializeField] private int amountItemDrops;
    public GameObject itemDropPrefab;
    public float speed;
    private float distanceOffSet = 1.5f;
    private Vector3 playerPos;
    private float timeAtk;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (amountItemDrops < 1)
        {
            Random.Range(0, 10);
        }
    }

    void Update()
    {
        timeAtk += Time.deltaTime;
        playerPos = player.transform.position;
        transform.LookAt(player.transform);
        float distanceToPlayer = Vector3.Distance(transform.position, playerPos);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer > distanceOffSet)
        {
            anim.SetBool("Running", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Running", false);
            if (timeAtk > stats.atkRate)
            {
                timeAtk = 0;
                damageable.SetActive(true);
                Invoke(nameof(Hit), 0.1f);
                anim.SetTrigger("Attack");
            }
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
            Dropeable();
            anim.SetBool("Die", true);
        }
        else
        {
            anim.SetTrigger("TakeDamage");
        }
    }

    public void Dropeable()
    {
        for (int i = 0; i < amountItemDrops; i++)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }
    }
}