using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterData
{
    public int Key;
    public int Type;
    public string Name;
    public float Hp;
    public float Attack;
    public float Defense;
    public float Critical;
    public float Speed;
    public float Cooltime;
    public string Grade;
    public string SkillName;
    public int Skill1;
    public int Skill2;
}
public struct CookieData
{
    public int Key;
    public int Type;
    public string Name;
    public int Level;
    public float Hp;
    public float LevelPerHp;
    public float LevelPerAttack;
    public float LevelPerDefense;
    public float LevelPerCritical;

}
public struct InventoryData
{
    public int Key;
    public int Type;
    public string Name;
    public int Level;
    public float Defense;

    public void LevelUp()
    {
        Level += 1;
    }
}

public struct WorldData
{
    public int Key;
    public int World;
    public int StageNum;
    public string BgImage;
}
public struct StageData
{
    public int Key;
    public int World;
    public int Stage;
    public bool IsUnlock;
    public int StarNum;
    public int Wave1;
    public int Wave2;
    public int Wave3;
}


public enum CharState
{
    Idle,
    Run,
    Skill1,
    Skill2,
    Skill3,
    Idle_front,
    Victory,
    Lose
}

public class DataManager : Singleton<DataManager>
{
    private Dictionary<int, CharacterData> _characterDatas = new Dictionary<int, CharacterData>();
    private Dictionary<int, CookieData> _cookieDatas = new Dictionary<int, CookieData>();
    private Dictionary<int, InventoryData> _inventoryDatas = new Dictionary<int, InventoryData>();
    //캐릭터 구현필요

    public static Dictionary<int, System.Type> skillMap = new Dictionary<int, System.Type>
    {
        { 101, typeof(BaseAttack) },
        { 102, typeof(CandyAttack) },
    };


    private Dictionary<int, WorldData> _worldData = new Dictionary<int, WorldData>();
    private Dictionary<int, StageData> _stageData = new Dictionary<int, StageData>();

    public WorldData GetWorldData(int key)
    {
        if (_worldData.TryGetValue(key, out WorldData data))
        {
            return data;
        }

        return default(WorldData);
    }

    public StageData GetStageData(int key)
    {
        if (_stageData.TryGetValue(key, out StageData data))
        {
            return data;
        }

        return default(StageData);
    }
    public CharacterData GetCharacterData(int key)
    {
        if( _characterDatas.TryGetValue(key,out CharacterData data))
        {
            return data;
        }
        return default(CharacterData);
    }
    public CookieData GetCookieData(int key)
    {
        if(_cookieDatas.TryGetValue(key,out CookieData data))
        {
            return data;
        }
        return default(CookieData);
    }

    public Dictionary<int, InventoryData> GetAllHaveCookieData()
    {
        return _inventoryDatas;
    }
    public List<CookieData> GetAllCookieData()
    {
        List<CookieData> list = new List<CookieData>();
        foreach (CookieData data in _cookieDatas.Values)
        {
            list.Add(data);
        }
        return list;
    }
    public List<CharacterData> GetAllCharacterData()
    {
        List<CharacterData> list = new List<CharacterData>();
        foreach (CharacterData data in _characterDatas.Values)
        {
            list.Add(data);
        }
        return list;
    }

    public void LoadAllData()
    {
        LoadWorldData();
        LoadStageData();
        LoadCookieData();
        LoadCharacterData();
        LoadInventoryData();
    }

    private void LoadInventoryData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/InventoryTable");

        if (textAsset == null)
        {
            Debug.LogError("WorldTable not found in Resources/Tables.");
            return;
        }
        string[] lines = textAsset.text.Split("\r\n");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            if (datas.Length <= 1) continue;
            InventoryData data;
            data.Key = int.Parse(datas[0]);
            data.Type = int.Parse(datas[1]);
            data.Name = datas[2];
            data.Level = int.Parse(datas[3]);
            data.Defense = float.Parse(datas[4]);

            _inventoryDatas.Add(data.Key, data);
        }
    }

    private void LoadCharacterData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/CharacterTable");

        if (textAsset == null)
        {
            Debug.LogError("WorldTable not found in Resources/Tables.");
            return;
        }
        string[] lines = textAsset.text.Split("\r\n");

        for (int i = 2; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            if (datas.Length <=1) continue;
            CharacterData data;
            data.Key = int.Parse(datas[0]);
            data.Type = int.Parse(datas[1]);
            data.Name = datas[2];
            data.Hp = float.Parse(datas[3]);
            data.Attack = float.Parse(datas[4]);
            data.Defense = float.Parse(datas[5]);
            data.Critical = float.Parse(datas[6]);
            data.Speed = float.Parse(datas[7]);
            data.Cooltime = float.Parse(datas[8]);
            data.Grade = datas[9];
            data.SkillName = datas[10];
            data.Skill1 = int.Parse(datas[11]);
            data.Skill2 = int.Parse(datas[12]);

            _characterDatas.Add(data.Key, data);
        }
    }
    private void LoadCookieData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/CookieTable");

        if (textAsset == null)
        {
            Debug.LogError("WorldTable not found in Resources/Tables.");
            return;
        }
        string[] lines = textAsset.text.Split("\r\n");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            if (datas.Length <=1) continue;
            CookieData data;
            data.Key = int.Parse(datas[0]);
            data.Type = int.Parse(datas[1]);
            data.Name = datas[2];
            data.Level = int.Parse(datas[3]);
            data.Hp = float.Parse(datas[4]);

            data.LevelPerHp = float.Parse(datas[5]);
            data.LevelPerAttack = float.Parse(datas[6]);
            data.LevelPerDefense = float.Parse(datas[7]);
            data.LevelPerCritical = float.Parse(datas[8]);
            


            _cookieDatas.Add(data.Key, data);
        }
    }
    private void LoadWorldData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/WorldTable");

        if (textAsset == null)
        {
            Debug.LogError("WorldTable not found in Resources/Tables.");
            return;
        }
        string[] lines = textAsset.text.Split("\r\n");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            if (datas.Length <= 1) continue;
            WorldData data;
            data.Key = int.Parse(datas[0]);
            data.World = int.Parse(datas[1]);
            data.StageNum = int.Parse(datas[2]);
            data.BgImage = datas[3];


            _worldData.Add(data.Key, data);
        }
    }

    private void LoadStageData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/StageTable");

        if (textAsset == null)
        {
            Debug.LogError("StageTable not found in Resources/Tables.");
            return;
        }
        string[] lines = textAsset.text.Split("\r\n");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            if (datas.Length <= 1) continue;
            StageData data;
            data.Key = int.Parse(datas[0]);
            data.World = int.Parse(datas[1]);
            data.Stage = int.Parse(datas[2]);
            data.IsUnlock = bool.Parse(datas[3]);
            data.StarNum = int.Parse(datas[4]);
            data.Wave1 = int.Parse(datas[5]);
            data.Wave2 = int.Parse(datas[6]);
            data.Wave3 = int.Parse(datas[7]);

            _stageData.Add(data.Key, data);
        }
    }
}
