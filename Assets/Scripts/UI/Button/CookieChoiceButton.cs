using UnityEngine;

public class CookieChoiceButton : ParentButton
{
    private int _key;
    private bool _isSet;
    private int _level;
    private CookieSettingManager _settingManager;

    public bool IsSet
    {
        get { return _isSet; }
        set { _isSet = value; }
    }
    public int Key
    {
        get { return _key; } 
        set { _key = value; }
    }
    public int Level
    {
        get { return _level; }
    }

    public void SetButton()
    {
        _isSet = false;
        _level = InventoryManager.Instance.GetData(Key).Level;
        ChangeImage();
    }
    protected override void OnButtonClick()
    {
        //위치 셋팅하고 넘기자 + 화면에 띄우기
        CharacterSetting();
    }
    private void Start()
    {
        _settingManager = GameObject.Find("CookieSettingManager").GetComponent<CookieSettingManager>();
    }
    private void CharacterSetting()
    {
        if (!_isSet)
        {
            _isSet = CharacterManager.Instance.SetCharacter(_key);
            if (!_isSet) return;
            _settingManager.SpawnCookies(_key);
        }
        else
        {
            _isSet = false;
            CharacterManager.Instance.SetOffCharacter(_key);
            _settingManager.DespawnCookies(_key);
        }
    }
    private void ChangeImage()
    {
        _image.sprite = Resources.Load<Sprite>("Textures/CharacterCard/cookie"+_key+"_card");
    }
}
