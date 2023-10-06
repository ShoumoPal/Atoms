using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* General class for object pools in the game. Mainly used for the particle effects */

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject obj, Vector3 spawnPos, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = ObjectPools.Find(i => i.LookUpString == obj.name);

        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookUpString = obj.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InActiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(obj, spawnPos, spawnRotation);
        }
        else
        {
            Debug.Log("Taking from pool");
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InActiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string name = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(i => i.LookUpString ==  name);

        if (pool == null)
            Debug.Log("Trying to return an object where pool is not created for : " + obj.name);
        else
        {
            obj.SetActive(false);
            pool.InActiveObjects.Add(obj);
        }
    }
}
public class PooledObjectInfo
{
    public string LookUpString;
    public List<GameObject> InActiveObjects = new List<GameObject>();
}
