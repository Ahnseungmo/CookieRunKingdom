using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

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
