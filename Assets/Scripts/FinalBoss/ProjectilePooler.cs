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
    public bool isLoaded;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj;

                if (pool.prefab.CompareTag("NPC"))
                {
                    obj = Instantiate(pool.prefab, GameObject.Find("Minion_Blank").transform);
                }
                else
                {
                    obj = Instantiate(pool.prefab, GameObject.Find("Projectiles_Blank").transform);
                }
                
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        isLoaded = true;
    }

    public GameObject SpawnFromPool(string type, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.Log("Pool with tag " + type + " doesn't exist.");
            return null;
        }
       
        GameObject objectToSpawn = poolDictionary[type].Dequeue();

        objectToSpawn.transform.position = position;

        if (type == "Minions")
        {
            objectToSpawn.GetComponentInChildren<Minion>().EnableMinion();
        }
        else
        {
            objectToSpawn.SetActive(true);
        }

        poolDictionary[type].Enqueue(objectToSpawn);
        
        return objectToSpawn;
    }
}
