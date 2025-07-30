using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookieSettingManager : MonoBehaviour
{
    private GameObject _buttonParent;
    private PoolingManager _buttons;
    private List<GameObject> _cookies;
    private List<CookieData> _cookieDatas;


    public void OnClickReset()
    {
        List<GameObject> button = _buttons.GetAllToActiveTrue();
        foreach (GameObject obj in button)
        {
            CookieChoiceButton btn = obj.GetComponent<CookieChoiceButton>();
            btn.IsSet = false;
        }
    }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _buttonParent = GameObject.Find("Panel/CookieButtons/Viewport/Content");
        _cookieDatas = DataManager.Instance.GetAllCookieData();
        CreateButtons();
        CreateCookies();
    }
    private void CreateButtons()
    {
        _buttons = new PoolingManager("Prefabs/Buttons/CookieCoiceButton", _buttonParent, _cookieDatas.Count);

        for (int i = 0; i < _cookieDatas.Count; i++)
        {
            CookieChoiceButton obj = _buttons.Pop().GetComponent<CookieChoiceButton>();
            obj.gameObject.SetActive(true);
            obj.Key = _cookieDatas[i].Key;
            obj.SetButton();
        }
        SortCookiesByLevelDesc();
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
      //  GameObject prefab = Resources.Load<GameObject>("Prefabs/StandCookie");
      //  GameObject parent = new GameObject("Cookies");
      //
      //  _cookies = new List<GameObject>(9);
      //
      //  for (int i = 0; i < 9; i++)
      //  {
      //      GameObject obj = Object.Instantiate(prefab, parent.transform);
      //      obj.SetActive(false);
      //      _cookies.Add(obj);
      //  }
    }
}
