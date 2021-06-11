using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public List<Vector3> positionsList;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            if(pool.tag == "Wall")
            {
                pool.size = (width * height) / 2;
                WallInitializer(pool.size, pool.prefab);
            }
            else if (pool.tag == "Floor")
            {
                FloorInitializer(pool.prefab);
            }
            else if(pool.tag == "BrickWall")
            {
                BrickWallInitializer(pool.size, pool.prefab);
            }
            else if (pool.tag == "Enemy")
            {
                EnemyInitializer(pool.size, pool.prefab);
            }
        }
    }
    void WallInitializer(int size, GameObject prefab)
    {
        for (int i = 0; i < (width * 2 ) + 1; i++)
        {
            if ((i % 2) == 0 && i > 0)
            {
                for (int j = 0; j < (height * 2 ) + 1; j++)
                {
                    if ((j % 2) == 0 && j > 0)
                    {
                        GameObject obj = Instantiate( prefab);
                        obj.transform.SetParent(gameObject.transform);
                        obj.transform.position = new Vector3(i, 0.5f, j);
                    }
                }
            }
        }
        for( int i = 0; i< 4; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(gameObject.transform);
            switch(i)
            {
                case 0:
                    obj.transform.position = new Vector3(0, 0.5f,height + 1);
                    obj.transform.localScale = new Vector3(1, 1, (height * 2) + 2);
                    break;
                case 1:
                    obj.transform.position = new Vector3((width * 2) +2, 0.5f, height + 1);
                    obj.transform.localScale = new Vector3(1, 1, (height * 2) + 3) ;
                    break;
                case 2:
                    obj.transform.position = new Vector3((width) + 1, 0.5f, 0);
                    obj.transform.localScale = new Vector3((width * 2) + 3, 1, 1);
                    break;
                case 3:
                    obj.transform.position = new Vector3((width) + 1, 0.5f, (height *2) +2);
                    obj.transform.localScale = new Vector3((width * 2) + 3, 1, 1);
                    break;
            }
        }
    }
    void FloorInitializer( GameObject prefab)
    {
        float thisHeight;
        float thisWidth;
        if (width < 4)
            thisWidth = 1;
        else
            thisWidth = width /4;

        if (height < 4)
            thisHeight = 1;
        else
            thisHeight = height / 4;
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(gameObject.transform);
        obj.transform.localScale = new Vector3( thisWidth , 1, thisHeight);
        obj.transform.position = new Vector3(width + 1, 0, height + 1);
    }
    void BrickWallInitializer(int size,GameObject prefab)
    {
        float x = 0;
        float z = 0;
        bool canSpawn = false;
        if(size ==0)
        {
            size = (width * height) / 4;
        }
        for (int i = 0; i < (size) + 1; i++)
        {
            do
            {
                x = Random.Range(2, ( width * 2 + 1));
                z = Random.Range(2, ( height * 2 + 1));
                if (positionsList == null && x % 2 == 0 && z % 2 == 0)
                    positionsList.Add(new Vector3(x, 0.5f, z));
                else
                {
                    for(int j = 0; j<positionsList.Count; j++ )
                    {
                        if (positionsList[j] == new Vector3(x, 0.5f, z))
                            canSpawn = false;
                        else
                        {
                            canSpawn = true;
                            break;
                        }
                    }
                }
            } while (x % 2 == 0 && z %2==0 && canSpawn == true);
            canSpawn = false;
            positionsList.Add(new Vector3(x, 0.5f, z));
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(gameObject.transform);
            obj.transform.position = new Vector3(x, 0.5f, z);

        }
    }

    void EnemyInitializer(int size ,GameObject prefab)
    {
        float x = 0;
        float z = 0;
        bool canSpawn = false;
        if(size ==0)
        {
            size = (width * height) / 8;
        }
        for (int i = 0; i < (size) + 1; i++)
        {
            do
            {
                x = Random.Range(2, (width * 2 + 1));
                z = Random.Range(2, (height * 2 + 1));
                if (positionsList == null && x % 2 == 0 && z % 2 == 0)
                    positionsList.Add(new Vector3(x, 0.5f, z));
                else
                {
                    for (int j = 0; j < positionsList.Count; j++)
                    {
                        if (positionsList[j] == new Vector3(x, 0.5f, z))
                            canSpawn = false;
                        else
                        {
                            canSpawn = true;
                            break;
                        }
                    }
                }
            } while (x % 2 == 0 && z % 2 == 0 && canSpawn == true);
            LevelManager.Get().StartEnemies();
            canSpawn = false;
            positionsList.Add(new Vector3(x, 0.5f, z));
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(gameObject.transform);
            obj.transform.position = new Vector3(x, 0.5f, z);
        }
    }
}
