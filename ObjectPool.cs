using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPool : MonoBehaviour
{
    Queue<GameObject> poolQueue = new Queue<GameObject>();
    public GameObject prefabInInspector;
    public int numberOfPrefab;
    private void Awake()
    {
        for (int i = 0; i < numberOfPrefab; i++)
        {
            GameObject tempVarPrefab;
            tempVarPrefab = Instantiate(prefabInInspector);
            poolQueue.Enqueue(tempVarPrefab);
            tempVarPrefab.SetActive(false);
        }
    }

    public GameObject Get(GameObject prefab, GameObject parent)
    {
        if (poolQueue.Count == 0)
        {
            Debug.Log("No GameObject in pool");
            GameObject instancedObj = Instantiate(prefab);
            instancedObj.transform.SetParent(parent);
            return instancedObj;
        }
        else
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(parent);
            return obj;
        }
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

}
