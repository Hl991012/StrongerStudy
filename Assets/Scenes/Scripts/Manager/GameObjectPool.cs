using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IRecyclable
{
    string ID { get; set; }
}
    
public class GameObjectPool : Singleton<GameObjectPool>
{
    private Dictionary<string, List<IRecyclable>> poolDict = new();

    public T GetFromPool<T>(string id) where T : MonoBehaviour, IRecyclable
    {
        T recyclableObj = null;
        if (poolDict.ContainsKey(id))
        {
            if (poolDict[id].Count > 0)
            {
                recyclableObj = (T)poolDict[id][0];
                poolDict[id].RemoveAt(0);
                if (poolDict[id].Count <= 0)
                {
                    poolDict.Remove(id);
                }
            }
        }

        var obj = Addressables.LoadAssetAsync<GameObject>(id).WaitForCompletion();
        recyclableObj = Object.Instantiate(obj).GetComponent<T>();
        return recyclableObj;
    }

    public void ReturnToPool(IRecyclable recyclableObj)
    {
        if (poolDict.ContainsKey(recyclableObj.ID))
        {
            poolDict[recyclableObj.ID].Add(recyclableObj);
        }
        else
        {
            poolDict.Add(recyclableObj.ID, new List<IRecyclable>(){recyclableObj});
        }
    }
}   
