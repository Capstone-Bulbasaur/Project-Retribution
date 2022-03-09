using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool willGrow;
    }

    public static ProjectilePooler Instance;

    private void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, GameObject.Find("Projectiles_Blank").transform);

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    public GameObject SpawnFromPool(string type, Vector3 position)
    {
        if (poolDictionary.ContainsKey(type))
        {
            Debug.Log("Type: " + type);
        }

        if (!poolDictionary.ContainsKey(type))
        {
            Debug.Log("Pool with tag " + type + " doesn't exist.");
            return null;
        }
        Debug.Log(type);
        GameObject objectToSpawn = poolDictionary[type].Dequeue();

        objectToSpawn.transform.position = position;
        
        objectToSpawn.SetActive(true);

        poolDictionary[type].Enqueue(objectToSpawn);
        
        return objectToSpawn;
    }
}
