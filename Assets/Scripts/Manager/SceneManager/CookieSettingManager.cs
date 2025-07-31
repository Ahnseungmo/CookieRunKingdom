using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CookieSettingManager : MonoBehaviour
{
    private GameObject _buttonParent;
    private PoolingManager _buttons;
    private PoolingManager _cookies;
    private List<InventoryData> _cookieDatas;
    private bool _isSetting = false;


    public void OnClickReset()
    {
        List<GameObject> button = _buttons.GetAllToActiveTrue();
        foreach (GameObject obj in button)
        {
            CookieChoiceButton btn = obj.GetComponent<CookieChoiceButton>();
            btn.IsSet = false;
        }
        List<GameObject> cookies = _cookies.GetAllToActiveTrue();
        foreach (GameObject cookie in cookies)
        {
           cookie.SetActive(false);
        }
    }
    public void SpawnCookies(int key)
    {
        GameObject obj = _cookies.Pop();
        PanelCharacter cookie = obj.GetComponent<PanelCharacter>();
        cookie.Key = key;

        List<GameObject> cookies = _cookies.GetAllToActiveTrue();
        AllCookiePositionSetting(cookies);
    }
    public void DespawnCookies(int key)
    {
        List<GameObject> cookies = _cookies.GetAllToActiveTrue();

        for (int i = 0; i < cookies.Count; i++)
        {
            PanelCharacter cooki = cookies[i].GetComponent<PanelCharacter>();
            if (cooki.Key == key)
            {
                cooki.gameObject.SetActive(false);
                cookies.RemoveAt(i);
                break;
            }
        }
        AllCookiePositionSetting(cookies);
    }
    private void AllCookiePositionSetting(List<GameObject> cookie)
    {
        Vector3 startPos = new Vector3(-3f, -1f, 0);
        Vector3 addPos = new Vector3(2.5f, 1f, 0); 
        foreach (GameObject obj in cookie)
        {
            PanelCharacter cooki = obj.GetComponent<PanelCharacter>();
            Index2 index = CharacterManager.Instance.GetPositionByKey(cooki.Key);
            cooki.transform.position = new Vector3(startPos.x+addPos.x * index.x, startPos.y+addPos.y * index.y, addPos.z);
        }
    }                            
    private void Start()
    {
        if(!_isSetting)
            Init();
    }
    private void OnEnable()
    {
        if (!_isSetting) return;
        SettingButton();
        OnClickReset();
        CharacterManager.Instance.ResetAll();
    }
    private void Init()
    {
        _buttonParent = GameObject.Find("Panel/CookieButtons/Viewport/Content");
        _cookieDatas = InventoryManager.Instance.GetAllHaveCookieData();
        CreateButtons();
        CreateCookies();
        _isSetting = true;
    }
    private void SettingButton()
    {
        List<GameObject> buttons = _buttons.GetAllToActiveTrue();
        for(int i=0;i<_cookieDatas.Count;i++)
        {
            buttons[i].SetActive(false);
        }

        _cookieDatas.Clear();
        _cookieDatas = InventoryManager.Instance.GetAllHaveCookieData();

        for(int i=0;i<_cookieDatas.Count;i++)
        {
            CookieChoiceButton obj = _buttons.Pop().GetComponent<CookieChoiceButton>();
            obj.gameObject.SetActive(true);
            obj.Key = _cookieDatas[i].Key;
            obj.SetButton();
        }
    }
    private void CreateButtons()
    {
        _buttons = new PoolingManager("Prefabs/Buttons/CookieCoiceButton", _buttonParent, _cookieDatas.Count);
        //SortCookiesByLevelAsc();
        for (int i = 0; i < _cookieDatas.Count; i++)
        {
            CookieChoiceButton obj = _buttons.Pop().GetComponent<CookieChoiceButton>();
            obj.gameObject.SetActive(true);
            obj.Key = _cookieDatas[i].Key;
            obj.SetButton();
        }

    }
    private void SortCookiesByLevelDesc() // 소팅하는 부분 다시 할것(제대로 동작 x)
    {
        List<GameObject> buttonList = _buttons.GetAllToActiveTrue()
            .OrderByDescending(b => b.GetComponent<CookieChoiceButton>().Level)
            .ToList();

        // 역순으로 SetSiblingIndex를 주면 깔끔하게 정렬됨
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].transform.SetSiblingIndex(buttonList.Count - 1); // 제일 뒤로 이동
         }

        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].transform.SetSiblingIndex(i);
        }
    }
    private void SortCookiesByLevelAsc()
    {
        _cookieDatas = _cookieDatas.OrderBy(cookie => cookie.Level).ToList();
    }

    private void SortCookiesByTypeAsc()
    {
        _cookieDatas = _cookieDatas.OrderBy(cookie => cookie.Type).ToList();
    }
    private void CreateCookies()
    {
      _cookies = new PoolingManager("Prefabs/Character/PanelCharacter", "Cookies", 9);
    }

    
}
