using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs;

    public GameObject GetGameObject(string type)
    {
        var localGameObject = objectPrefabs.FirstOrDefault(x => x.name == type);

        if (localGameObject == null)
            return null;

        GameObject newObject = Instantiate(localGameObject);
        newObject.name = type;

        return newObject;
    }
}
