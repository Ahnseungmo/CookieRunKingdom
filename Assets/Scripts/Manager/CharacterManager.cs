using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CharacterManager : Singleton<CharacterManager>
{
    private List<int> _setCharacterList = new List<int>(); 

    public List<int> Character
    { get { return _setCharacterList; } }

    public void SetCharacter(int key)//이렇게 삽입하면 앞부터 채워진다 고민함 해보자
    {
        bool isInsert = false;
        CharacterData data = DataManager.Instance.GetCharacterData(key);
        int count = -1;
        foreach (int node in _setCharacterList)
        {
            count++;
            CharacterData nodeData = DataManager.Instance.GetCharacterData(node);

            if (data.Defense <= nodeData.Defense) continue;

            _setCharacterList.Insert(count, key);
            isInsert = true;
            break;
        }
        if(!isInsert)
            _setCharacterList.Add(key);

        foreach (int i in _setCharacterList)
        {
            Debug.Log(i+"/"+DataManager.Instance.GetCharacterData(i).Defense);
        }
    }
    public void SetOffCharacter(int key)
    {
        _setCharacterList.Remove(key);
        foreach (int i in _setCharacterList)
        {
            Debug.Log(i);
        }
    }
    public void ResetAll()
    {
        _setCharacterList.Clear();

    }
}