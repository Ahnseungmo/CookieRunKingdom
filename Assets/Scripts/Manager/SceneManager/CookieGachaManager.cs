using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Sprites;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public enum CharacterGrade
{
    Common, Rare, Special
}
public class CookieGachaManager : MonoBehaviour
{
    private VideoPlayer _normalPlayer;
    private VideoPlayer _luxuryPlayer;
    private RawImage _display;
    private SpriteRenderer _backGround;
    private SpriteRenderer _cookie;
    private Dictionary<CharacterGrade, List<int>> _keyList = new Dictionary<CharacterGrade, List<int>>();

    public void OnClickNormalButton()
    {
        _display.gameObject.SetActive(true);
        _normalPlayer.Play();
        GachaCookie("normal");
    }
    public void OnClickLuxuryButton()
    {
        _display.gameObject.SetActive(true);
        _luxuryPlayer.Play();
        GachaCookie("luxury");

    }
    public void OnClickExitButton()
    {
        if (_display.gameObject.activeSelf)
        {
            OnVideoEnd(null);
            return;
        }
        SceneManager.LoadScene("LobyScene");
        
    }
    private void Start()
    {
        _normalPlayer = GameObject.Find("Panel/Player/NormalGachaVideo").GetComponent<VideoPlayer>();
        _luxuryPlayer = GameObject.Find("Panel/Player/LuxuryGachaVideo").GetComponent<VideoPlayer>();
        _normalPlayer.loopPointReached += OnVideoEnd;
        _luxuryPlayer.loopPointReached += OnVideoEnd;

        _display = GameObject.Find("Panel/Player").GetComponent<RawImage>();
        _display.gameObject.SetActive(false);

        _backGround = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        _cookie = GameObject.Find("GachaCookie").GetComponent<SpriteRenderer>();

        _cookie.gameObject.SetActive(false);

        SettingKeyDictionary();
    }

    private void SettingKeyDictionary()
    {
        List<CharacterData> characterList = DataManager.Instance.GetAllCharacterData();

        List<int> commonKeyList = new List<int>();
        List<int> rareKeyList = new List<int>();
        List<int> specialKeyList = new List<int>();
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].Grade == "Common")
            {
                commonKeyList.Add(characterList[i].Key);
            }
            else if (characterList[i].Grade == "Rare")
            {
                rareKeyList.Add(characterList[i].Key);
            }
            else if (characterList[i].Grade == "Special")
            {
               specialKeyList.Add(characterList[i].Key);
            }
        }
        _keyList.Add(CharacterGrade.Common, commonKeyList);
        _keyList.Add(CharacterGrade.Rare, rareKeyList);
        _keyList.Add(CharacterGrade.Special, specialKeyList);

    }

    private void GachaCookie(string type)
    {
        int common = 0;
       switch(type)
        {
            case "normal":
                common = 7;
                break;
            case "luxury":
                common = 4;
                break;
        }
        int rare = (10 - common) / 3 * 2;
        int special = (10 - common) / 3;

        int randNum = Random.Range(0, 10);
        int key;
        if (randNum < common)
        {
            key = _keyList[CharacterGrade.Common][Random.Range(0, _keyList[CharacterGrade.Common].Count)];
        }
        else if (randNum - common < rare)
        {
            key = _keyList[CharacterGrade.Rare][Random.Range(0, _keyList[CharacterGrade.Rare].Count)];
        }
        else
        {
            key = _keyList[CharacterGrade.Special][Random.Range(0, _keyList[CharacterGrade.Special].Count)];
        }
        _cookie.sprite = Resources.Load<Sprite>("Textures/CharacterCard/cookie" + key + "_card");
        InventoryManager.Instance.LevelUp(key);

        List<InventoryData> list  = InventoryManager.Instance.GetAllHaveCookieData();
        print("---------------------------");
        for(int i=0;i<list.Count; i++)
            print(list[i].Key);
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        _backGround.sprite = Resources.Load<Sprite>("Textures/CharacterGachaScene/GachaBackGround");
        _display.gameObject.SetActive(false);
        _cookie.gameObject.SetActive(true);
    }
}
