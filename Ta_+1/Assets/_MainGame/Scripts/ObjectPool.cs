using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField]
    private GameObject[] objectPrefabs;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject GetObject(string type)
    {
        var pooledObject = pooledObjects.FirstOrDefault(x => x.name == type && !x.activeInHierarchy);

        if (pooledObject != null)
        {
            pooledObject.SetActive(true);
            return pooledObject;
        }

        var localGameObject = objectPrefabs.FirstOrDefault(x => x.name == type);

        if (localGameObject == null)
            return null;

        GameObject newObject = Instantiate(localGameObject);
        newObject.name = type;

        pooledObjects.Add(newObject);

        return newObject;
    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
