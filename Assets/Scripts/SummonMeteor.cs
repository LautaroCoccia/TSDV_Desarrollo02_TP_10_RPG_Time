using UnityEngine;

public class SummonMeteor : MonoBehaviour
{
    [SerializeField] GameObject meteoriteBase;
    [SerializeField] float meteoriteRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            InstanciateMeteor();
        }
    }

    void InstanciateMeteor()
    {
        Vector3 newPosition = transform.position + (transform.forward * meteoriteRange);
        Instantiate(meteoriteBase, newPosition, Quaternion.identity);
    }
}
