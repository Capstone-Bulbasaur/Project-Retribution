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

    //public GameObject pooledObject;
    //public int pooledAmount;
    //public bool willGrow;
    //private List<GameObject> pooledObjects;

    #region Singleton
    public static ProjectilePooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

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
                GameObject obj = Instantiate(pool.prefab, GameObject.Find("Projectiles_Blank").transform);

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        //pooledObjects = new List<GameObject>();
        //for (int i = 0; i < pooledAmount; i++)
        //{
        //    GameObject obj = Instantiate(pooledObject);
        //    obj.SetActive(false);
        //    pooledObjects.Add(obj);
        //}
    }

    public Transform SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        //Debug.Log("Projectile Position is: " + position);
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        Debug.Log("Shoot Position: " + position);
        //objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn.transform;


        //for (int i = 0; i < pooledObjects.Count; i++)
        //{
        //    if (!pooledObjects[i].activeInHierarchy)
        //    {
        //        pooledObjects[i].transform.position = position;
        //        pooledObjects[i].transform.rotation = rotation;
        //        pooledObjects[i].SetActive(true);

        //        return pooledObjects[i];
        //    }
        //}

        //if (willGrow)
        //{
        //    GameObject obj = Instantiate(pooledObject);
        //    pooledObjects.Add(obj);
        //    return obj;
        //}

        //return null;
    }

}

//public class ProjectilePooler : MonoBehaviour {
//    [Serializable] // Let this appear in the inspector.
//    public class ObjectPool {
//        public int amount;
//        public PooledObject objectToPool;
//    }

//    // Singleton boilerplate.
//    private static ProjectilePooler _instance;
//    public static ProjectilePooler Instance {
//        get {
//            if (!_instance) {
//                _instance = FindObjectOfType<ProjectilePooler>();
//            }

//            return _instance;
//        }
//    }

//    // List of objects to pool (only used for instantiation)
//    public List<ObjectPool> objectPools;

//    // Pool of objects, indexed by instance ID.
//    // A queue works pretty naturally here.
//    private Dictionary<int, Queue<PooledObject>> pool;

//    private void Awake() {
//        // Spawn all objects in provided pools.
//        pool = new Dictionary<int, Queue<PooledObject>>();
//        foreach (ObjectPool objPool in objectPools) {
//            int amount = objPool.amount;
//            PooledObject obj = objPool.objectToPool;

//            // Saved prefabs have an instance id, which we can use to talk about the same prefab from other scripts.
//            int id = obj.GetInstanceID();
//            Queue<PooledObject> queue = new Queue<PooledObject>(amount);
//            for (int i = 0; i < amount; i++) {
//                var clone = Instantiate(obj, transform);
//                clone.id = id;
//                clone.Finished += ReQueue;      // When `Finish()` is called, we put our object back in the queue.
//                clone.gameObject.SetActive(false);
//                queue.Enqueue(clone);
//            }

//            pool.Add(id, queue);
//        }
//    }

//    private PooledObject GetNextObject(PooledObject obj) {
//        // @NOTE: create a new queue if none exists, for pooling "unplanned" objects?
//        var queue = pool[obj.GetInstanceID()];
//        PooledObject clone = null;
//        // If queue is empty (has been exhausted -- the pool size was too small), extend the queue by instantiating a new object,
//        // and add it to the future queue.
//        if (queue.Count == 0) {
//            Debug.LogWarning("Object Pool queue was empty; wasn't able to get a new pooled object, so one will be instatiated.");
//            clone = Instantiate(obj, transform);
//            clone.id = obj.GetInstanceID();
//            clone.Finished += ReQueue;
//            clone.gameObject.SetActive(false);
//        } else {
//            clone = queue.Dequeue();
//        }

//        return clone;
//    }

//    // Gets an object from the pool and returns it after setting position, rotation, and active.
//    public PooledObject Spawn(PooledObject obj, Vector3 position, Quaternion rotation) {
//        var clone = GetNextObject(obj);
//        clone.transform.position = position;
//        clone.transform.rotation = rotation;
//        clone.gameObject.SetActive(true);
//        return clone;
//    }

//    private void ReQueue(PooledObject obj) {
//        // Hide object and insert back in queue for reuse.
//        obj.gameObject.SetActive(false);
//        var queue = pool[obj.id];
//        queue.Enqueue(obj);
//    }
//}


