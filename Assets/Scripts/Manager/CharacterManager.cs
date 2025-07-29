using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class CharacterManager : Singleton<CharacterManager>
{
    private List<int> _setCharacterList = new List<int>();
    private int _characterCount = 0;
    private int[] _characterArr = new int[9];

    public List<int> Character
    { get { return _setCharacterList; } }

    public void SetCharacter(int key)
    {
        if (_characterCount == 5) return;
        _characterCount++;
        CookieData cookie = DataManager.Instance.GetCookieData(key);
        
        switch (cookie.Type)
        {
            case 1: //전방
                SettingPosition(1,key);
                break;
            case 2: //중앙
                SettingPosition(2, key);
                break;
            case 3://후방
                SettingPosition(3, key);
                break;

        }

        for(int i=0;i<9;i++)
        {
            Debug.Log(_characterArr[i]);
            if(i%3 == 2)
                Debug.Log("---------------------");
        }
        Debug.Log("끝");
    }
    //public void SetCharacter(int key)//이렇게 삽입하면 앞부터 채워진다 고민함 해보자
    //{
    //    bool isInsert = false;
    //    CharacterData data = DataManager.Instance.GetCharacterData(key);
    //    int count = -1;
    //    foreach (int node in _setCharacterList)
    //    {
    //        count++;
    //        CharacterData nodeData = DataManager.Instance.GetCharacterData(node);
    //
    //        if (data.Defense <= nodeData.Defense) continue;
    //
    //        _setCharacterList.Insert(count, key);
    //        isInsert = true;
    //        break;
    //    }
    //    if (!isInsert)
    //        _setCharacterList.Add(key);
    //
    //    foreach (int i in _setCharacterList)
    //    {
    //        Debug.Log(i + "/" + DataManager.Instance.GetCharacterData(i).Defense);
    //    }
    //}
    public void SetOffCharacter(int key)
    {
        _characterCount--;
        for(int i=0;i<9;i++)
        {
            if(_characterArr[i] == key)
                _characterArr[i] = 0;
            SetOffPosition(i);
            return;
        }


    }
    public void ResetAll()
    {
        for(int i=0;i<9;i++)
        {
            _characterArr[i] = 0;
        }
    }

    private void SetOffPosition(int num)
    {
        if (num % 3 == 1) return;

        if(num % 3 == 2)
        {
            _characterArr[num - 1] = _characterArr[num - 2];
            _characterArr[num - 2] = 0;
        }
        else
        {
            _characterArr[num + 1] = _characterArr[num + 2];
            _characterArr[num + 2] = 0;
        }
    }
    private void SettingPosition(int num, int key)
    {
        num = (num - 1)*3;

        //3*3 배열이라 9 =>이거 상수로 빼자
        if (_characterArr[num + 1] == 0)
        {
            _characterArr[num + 1] = key;
            return;
        }

        else if (_characterArr[num + 1] !=0)
        {
            CharacterData originData = DataManager.Instance.GetCharacterData(num + 1);
            CharacterData keyData = DataManager.Instance.GetCharacterData(key);

            if(keyData.Defense>originData.Defense)
            {
                _characterArr[num] = key;
                _characterArr[num + 2] = _characterArr[num + 1];
                _characterArr[num + 1] = 0;
            }
            else
            {
                _characterArr[num] = _characterArr[num + 1];
                _characterArr[num + 1] = 0;
                _characterArr[num + 2] = key;
            }
            return;
        }
        //num+2랑 비교후 key값 바꿔서 넘기기

        //여기부터 코드 개 난잡해질것같아서 일단 보류. 예외처리 엄청 많이 필요하다. 

        if (num!=6 && _characterArr[num+5] ==0)
        {
            CharacterData originData = DataManager.Instance.GetCharacterData(num + 2);
            CharacterData keyData = DataManager.Instance.GetCharacterData(key);
            int temp;

            if (keyData.Defense > originData.Defense)
            {
                temp = _characterArr[num + 2];
                _characterArr[num + 2] = key;
                num = num / 3;
            }
            else
                temp = key;
            SettingPosition(num + 1, temp);
            return;
        }
            
        else if(num!=0 && _characterArr[num-1] == 0)
        {
            CharacterData originData = DataManager.Instance.GetCharacterData(num);
            CharacterData keyData = DataManager.Instance.GetCharacterData(key);
            int temp;

            if (keyData.Defense < originData.Defense)
            {
                temp = _characterArr[num];
                _characterArr[num] = key;
            }
            else
                temp = key;
            SettingPosition(num + 1, temp);
            return;
        }

        num /= 3;
        // 다 찼을때 미는코드
        if (num != 0)
            SettingPosition(num - 1, key);
        else if(num!=6)
            SettingPosition(num + 1, key);
    }
}