using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pool = new List<GameObject>();

    private int amountToPool = 5;

    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            obj.transform.parent = GameObject.Find("canon").transform;
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject() 
    {
        for(int i = 0;i < pool.Count;i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
