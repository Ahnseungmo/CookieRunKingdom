using UnityEngine;

public class CookieChoiceButton : ParentButton
{
    private int _key;
    private bool _isSet;

    public bool IsSet
    {
        get { return _isSet; }
        set { _isSet = value; }
    }
    public int Key
    { get { return _key; } 
      set { _key = value; }
    }

    public void SetButton()
    {
        _isSet = false;
        ChangeImage();
    }
    protected override void OnButtonClick()
    {
        //위치 셋팅하고 넘기자 + 화면에 띄우기
        CharacterSetting();

    }
    private void CharacterSetting()
    {
        if (!_isSet)
        {
            CharacterManager.Instance.SetCharacter(_key); //깊이감 줘야되는데 넣으면서 정렬을할까? 
            _isSet = true;
        }
        else
        {
            _isSet = false;
            CharacterManager.Instance.SetOffCharacter(_key);
        }
    }
    private void ChangeImage()
    {
        _image.sprite = Resources.Load<Sprite>("Textures/CharacterCard/cookie"+1001+"_card");
    }
}
