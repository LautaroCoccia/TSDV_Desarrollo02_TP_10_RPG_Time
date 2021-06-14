using UnityEngine;

public class CameraPlayer: MonoBehaviour
{
    private Player player;

    private Vector3 initialPos;
    private Vector3 playerPos;
    private Vector3 distance;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        playerPos = player.transform.position;
        initialPos = transform.position;
        distance = SumTwoVectors(playerPos, initialPos, false);
    }

    void Update()
    {
        playerPos = player.transform.position;
        transform.position = SumTwoVectors(playerPos, distance, false);
    }

    Vector3 SumTwoVectors(Vector3 ini, Vector3 end, bool sum)
    {
        Vector3 ret;
        if (sum)
            ret = new Vector3(ini.x + end.x, ini.y + end.y, ini.z + end.z);
        else
            ret = new Vector3(ini.x - end.x, ini.y - end.y, ini.z - end.z);

        return ret;
    }
}
