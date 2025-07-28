using System.Collections.Generic;
using UnityEngine;

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
}
public struct CookieData
{
    public int Key;
    public int Type;
    public string Name;
    public float Hp;
    public float Attack;
    public float Defense;
    public float Critical;
    public float LevelPerHp;
    public float LevelPerAttack;
    public float LevelPerDefense;
    public float LevelPerCritical;

    public float Speed;

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

public class DataManager : Singleton<DataManager>
{
    private Dictionary<int, CharacterData> _characterDatas = new Dictionary<int, CharacterData>();

    //캐릭터 구현필요



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

    public void LoadAllData()
    {
        LoadWorldData();
        LoadStageData();
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
