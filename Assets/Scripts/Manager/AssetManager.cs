using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : Singleton<AssetManager>
{
    public enum AssetStoreType
    {
        Resources,
        AssetBundle,
        Addressable,
        Editor,
    }
    
    public T LoadAsset<T>(string name, AssetStoreType assetStoreType) where T : Object
    {
        T t = null;
        switch (assetStoreType)
        {
            case AssetStoreType.Resources:
                t = (T)Resources.LoadAsync<T>(name).asset;
                break;
            case AssetStoreType.Addressable:
                break;
            case AssetStoreType.AssetBundle:
                break;
            case AssetStoreType.Editor:
                break;
        }
        return t;
    }
}
