using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : Singleton<InventoryManager>
{
    private Dictionary<int,InventoryData> _data;


    public void LevelUp(int key)
    {
        if (!_data.ContainsKey(key))
            AddCookie(key);
        else
            CookieLevelUp(key);

    }
    public void SetData() //게임매니저에서 해주기
    {
        _data = DataManager.Instance.GetAllHaveCookieData();
    }

    public InventoryData GetData(int key)
    {
        return _data[key];
    }
    public List<InventoryData> GetAllHaveCookieData()
    {
        List<InventoryData> list = new List<InventoryData>();
        foreach (InventoryData data in _data.Values)
        {
            list.Add(data);
        }
        list = list.OrderByDescending(x => x.Level).ToList();
        return list;
    }
    private void CookieLevelUp(int key)
    {
        _data[key].LevelUp();
    }
    private void AddCookie(int key)
    {
        InventoryData data = new InventoryData();
        data.Key = key;
        data.Type = DataManager.Instance.GetCookieData(key).Type;
        data.Name = DataManager.Instance.GetCookieData(key).Name;
        data.Level = 1;
        data.Defense = DataManager.Instance.GetCharacterData(key).Defense;

        _data.Add(key, data);
    }
}
