using UnityEngine;
using System;
public class Meteorite : MonoBehaviour
{
    public static event Action<Meteorite> MeteoriteDeath;

    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, UnityEngine.Random.Range(minForce, maxForce), 0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Destruir GameObject o Hacer Daño
        }
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        MeteoriteDeath(this);
    }
}
