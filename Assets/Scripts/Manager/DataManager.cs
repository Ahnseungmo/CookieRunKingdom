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

    public class DataManager : Singleton<DataManager>
{
    private Dictionary<int, CharacterData> _characterDatas = new Dictionary<int, CharacterData>();

    //구현필요
}
