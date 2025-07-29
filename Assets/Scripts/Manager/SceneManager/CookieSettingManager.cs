using UnityEngine;
using System.Collections.Generic;

public class CookieSettingManager : MonoBehaviour
{
    private GameObject _buttonParent;
    private PoolingManager _buttons;
    private List<CookieData> _cookieDatas;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _buttonParent = GameObject.Find("CookiesPanel/CookieButtons/Viewport/Content");
        _cookieDatas = DataManager.Instance.GetAllCookieData();
        CreateButtons();
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
}
