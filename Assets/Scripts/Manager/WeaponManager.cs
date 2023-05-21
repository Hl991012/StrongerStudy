using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    private SaveModel saveModel = new();
    
    private class SaveModel
    {
        [JsonProperty("cur_weapon_id")]
        public string curWeaponId = ""; //当前使用的武器ID

        [JsonProperty("main_weapon_models")]
        public SerializableDictionary<string, MainWeaponModel> mainWeaponModelDict = new();
    }

    public async void Init()
    {
        //数据初始化
        saveModel = await GameHelper.LoadDate<SaveModel>(GameHelper.DataSavePath + nameof(WeaponManager)); 
        //使用默认配置
        if (saveModel.curWeaponId is (null or ""))
        { 
            saveModel.curWeaponId = SoManager.Instance.defaultConfigSo.defaultMainWeaponId;
        }
    }
    

    public void Save()
    { 
        GameHelper.SaveData(GameHelper.DataSavePath + nameof(WeaponManager), saveModel);
    }
}
