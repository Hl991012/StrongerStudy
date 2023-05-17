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

    public IRecyclable GetFromPool(string id)
    {
        IRecyclable recyclableObj = null;
        if (poolDict.ContainsKey(id))
        {
            if (poolDict[id].Count > 0)
            {
                recyclableObj = poolDict[id][0];
                poolDict[id].RemoveAt(0);
                if (poolDict[id].Count <= 0)
                {
                    poolDict.Remove(id);
                }
            }
        }

        recyclableObj = Addressables.LoadAssetAsync<IRecyclable>(id).WaitForCompletion();
        
        
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
