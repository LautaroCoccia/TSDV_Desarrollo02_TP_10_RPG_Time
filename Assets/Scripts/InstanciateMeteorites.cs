using UnityEngine;

public class InstanciateMeteorites : MonoBehaviour
{
    [SerializeField] int meteoriteAmount;
    [SerializeField] GameObject meteorite;
    [SerializeField] float Radius;
    [SerializeField] int currentMeteoriteAmount;

    void Start()
    {
        for (int i = 0; i < meteoriteAmount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.insideUnitSphere.x * Radius, 10, Random.insideUnitSphere.z * Radius);
            Instantiate(meteorite, randomPosition, Quaternion.identity, transform);
            Meteorite.MeteoriteDeath += OnMeteoriteDestruction;
        }
    }
    void OnMeteoriteDestruction(Meteorite meteorite) 
    {
        currentMeteoriteAmount--; 
        if(currentMeteoriteAmount<=0)
        {
            Destroy(this);
        }

    }
    //Aca falta darle una accion al meteorito para decirle a esto cuando puede destruirse (capaz se puede hacer algo tipo OnDestroy() con meteoriteAmount va bajando, y cuando llega a 0 haces destroy(this)
}
