using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class WorldDataManager:Singleton<WorldDataManager>
{
    private Dictionary<int,WorldData> _worldData;
    private Dictionary<int,StageData> _stageData;
    private int _worldKey;
    private int _stageKey;

    public int WorldKey
    {
        get { return _worldKey; }
        set { _worldKey = value; }
    }
    public int StageKey
    {
        get { return _stageKey; }
        set { _stageKey = value; }
    }
    public WorldData GetWorldData()
    {
        if (_worldData.TryGetValue(_worldKey, out WorldData data))
        {
            return data;
        }
        return default(WorldData);
    }
    public WorldData GetWorldData(int key)
    {
        if (_worldData.TryGetValue(key, out WorldData data))
        {
            return data;
        }
        return default(WorldData);
    }
    public StageData GetStageData()
    {
        if (_stageData.TryGetValue(_stageKey, out StageData data))
        {
            return data;
        }
        return default(StageData);
    }
    public StageData GetStageData(int key)
    {
        if (_stageData.TryGetValue(key, out StageData data))
        {
            return data;
        }
        return default(StageData);
    }
    public int GetWorldCount()
    {
        return _worldData.Count;
    }
    
    

    public void SetData()
    {
        _worldData = DataManager.Instance.GetAllWorldData();
        _stageData = DataManager.Instance.GetAllStageData();
    }

}
