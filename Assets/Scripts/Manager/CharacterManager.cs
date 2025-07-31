using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public struct Index2
{
    public int y;
    public int x;

    public Index2(int y, int x)
    {
        this.y = y;
        this.x = x;
    }
}
public class CharacterManager : Singleton<CharacterManager>
{
    private HashSet<int> _setCharacterList = new HashSet<int>();
    private int _characterCount = 0;
    private int[,] _characterArr = new int[3,3];
    private bool _isFull = false;

    private int _characterTypeNum = 0;
    public HashSet<int> Character
    { get { return _setCharacterList; } }
    public int[,] CharacterArr
    {
        get { return _characterArr; }
    }

    public bool SetCharacter(int key)
    {
        if (_characterCount == 5) return false;
        _characterCount++;
        InventoryData cookie = InventoryManager.Instance.GetData(key);
        
        switch (cookie.Type)
        {
            case 1: //전방
                _characterTypeNum = 1;
                SettingPosition(2,key);
                break;
            case 2: //중앙
                _characterTypeNum = 2;
                SettingPosition(1, key);
                break;
            case 3://후방
                _characterTypeNum = 3;
                SettingPosition(0, key);
                break;

        }

 
        _setCharacterList.Add(key);
        Debug.Log(_characterArr[0, 0] + "|" + _characterArr[0, 1] + "|" + _characterArr[0, 2]);
        Debug.Log(_characterArr[1, 0] + "|" + _characterArr[1, 1] + "|" + _characterArr[1, 2]);
        Debug.Log(_characterArr[2, 0] + "|" + _characterArr[2, 1] + "|" + _characterArr[2, 2]);
        Debug.Log("---------------------------------------------------");
        return true;
    }
    public Index2 GetPositionByKey(int key)
    {
        for(int y=0;y<3;y++)
            for(int x=0;x<3;x++)
            {
                if (_characterArr[y, x] == key) return new Index2(y, x);
            }
        return new Index2(3,3);
    }
    public void SetOffCharacter(int key)
    {
        _characterCount--;
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (_characterArr[y, x] == key)
                {
                    _characterArr[y, x] = 0;
                    _setCharacterList.Remove(key);
                    SetOffPosition(y,x);
                    //return;
                }

        Debug.Log(_characterArr[0, 0] + "|" + _characterArr[0, 1] + "|" + _characterArr[0, 2]);
        Debug.Log(_characterArr[1, 0] + "|" + _characterArr[1, 1] + "|" + _characterArr[1, 2]);
        Debug.Log(_characterArr[2, 0] + "|" + _characterArr[2, 1] + "|" + _characterArr[2, 2]);
        Debug.Log("---------------------------------------------------");
    }
    public void ResetAll()
    {
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                _characterArr[y,x] = 0;
        _setCharacterList.Clear();
        _characterCount = 0;
    }

    private void SetOffPosition(int y,int x)
    {
        if (y == 1) return;
        HashSet<int> temp = new HashSet<int>(_setCharacterList);
        ResetAll();

        foreach (int key in temp)
        {
            SetCharacter(key);
        }
        //else if(y == 2)
        //{
        //    _characterArr[1,x] = _characterArr[0, x];
        //    _characterArr[0, x] = 0;
        //}
        //else
        //{
        //    _characterArr[1,x] = _characterArr[2, x];
        //    _characterArr[2, x] = 0;
        //}
    }
    private void SettingPosition(int num, int key)
    {

        if (_characterArr[1,num] == 0 && _characterArr[0,num] == 0)
        {
            _characterArr[1, num] = key;
            return;
        }
        else if (_characterArr[1, num]!=0)
        {
            InventoryData originData = InventoryManager.Instance.GetData(_characterArr[1, num]);
            InventoryData keyData = InventoryManager.Instance.GetData(key);

            if (originData.Defense>keyData.Defense)
            {
                _characterArr[0, num] = _characterArr[1, num];
                _characterArr[1, num] = 0;
                _characterArr[2, num] = key;
            }
            else
            {
                _characterArr[0, num] = key;
                _characterArr[2, num] = _characterArr[1, num];
                _characterArr[1, num] = 0;
            }
            return;
        }

        if(_characterTypeNum ==2)
        {
            if (_characterArr[0, num - 1] != 0 && _characterArr[2, num - 1] != 0)
            {
                _isFull = true;
            }
        }

        if (num > 0 && _characterTypeNum != 3 && !_isFull)
        {
            List<InventoryData> data = new List<InventoryData>();
            data.Add(InventoryManager.Instance.GetData(_characterArr[0, num]));
            data.Add(InventoryManager.Instance.GetData(_characterArr[2, num]));
            data.Add(InventoryManager.Instance.GetData(key));
            float min = data[2].Defense;
            int temp = key;

            for(int i=0;i<2;i++)
            {
                if (data[i].Defense < min)
                    min = data[i].Defense;
            }
            
            if (data[0].Defense == min)
            {
                temp = _characterArr[0, num];
                _characterArr[0, num] = key;
            }
            else if (data[1].Defense == min)
            {
                temp = _characterArr[2, num];
                _characterArr[2, num] = key;
            }

            SettingPosition(num - 1, temp);
            Debug.Log("1");
            return;

        }
        else if (num < 2 && _characterTypeNum != 1)
        {
            Debug.Log("3");
            List<InventoryData> data = new List<InventoryData>();
            data.Add(InventoryManager.Instance.GetData(_characterArr[0, num]));
            data.Add(InventoryManager.Instance.GetData(_characterArr[2, num]));
            data.Add(InventoryManager.Instance.GetData(key));
            float max = data[2].Defense;
            int temp = key;

            for(int i=0;i<2;i++)
            {
                if(data[i].Defense > max)
                    max = data[i].Defense;
            }

            if (data[0].Defense == max)
            {
                temp = _characterArr[0, num];
                _characterArr[0, num] = key;
            }
            else if (data[1].Defense == max)
            {
                temp = _characterArr[2, num];
                _characterArr[2, num] = key;
            }
            _isFull = false;
            SettingPosition(num + 1, temp);
            return;
        }
    }
}