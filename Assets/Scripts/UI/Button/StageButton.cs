using UnityEngine;

public class StageButton: ParentButton
{
    StageData _data;
    public StageData Data
    {
        get { return _data; } 
        set { _data = value; }
    }
    public void SetInformation()
    {
        ChangeImage();
        _text.text = _data.World + "-" + _data.Stage;
    }

    private void ChangeImage()
    {
        if (!_data.IsUnlock)
            _image.sprite = Resources.Load<Sprite>("Textures/StageScene/button_navy");
        else if (_data.StarNum == 0)
            _image.sprite = Resources.Load<Sprite>("Textures/StageScene/button_blue");
        else
            _image.sprite = Resources.Load<Sprite>("Textures/StageScene/button_yellow");
    }

    protected override void OnButtonClick()
    {
        if (!_data.IsUnlock) return;

        print(_data.Stage); // 스테이지 정보 gameManager로 넘기고 씬 넘겨주기
    }
}
