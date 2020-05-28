using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;

    public static ObjectPool GetInstance()
    {
        if (instance == null)
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
        }
        return instance;
    }

    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        StartCoroutine(Load(name, callback));
    }

    private IEnumerator Load<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest Object = Resources.LoadAsync<T>(name);
        yield return Object;
        if (Object.asset is GameObject)
        {
            callback(GameObject.Instantiate(Object.asset) as T);

        }
        else
        {
            callback(Object as T);
        }
    }
}

public class PoolData
{
    public GameObject fatherObj;
    public List<GameObject> poolList/* = new List<Object>()*/;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform.parent;
        poolList = new List<GameObject>() { };
        Push(obj);
    }

    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
    }

    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(false);
        obj.transform.parent = null;
        return obj;
    }
}

public class Pool : BaseManeger<Pool>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject poolObj;

    public void PushObj(string name, GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("pool");
            if (poolDic.ContainsKey(name))
            {
                poolDic[name].Push(obj);

            }
            else
            {
                poolDic.Add(name, new PoolData(obj, poolObj));
            }
        }
    }

    public GameObject GetObj(string name, UnityAction<GameObject> callback)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            callback(poolDic[name].GetObj());
        }
        else
        {
            ObjectPool.GetInstance().LoadAsync<GameObject>(name, (obj) => { obj.name = name; callback(obj); });
        }
    }

    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}


