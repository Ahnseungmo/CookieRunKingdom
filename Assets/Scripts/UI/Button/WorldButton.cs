using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : ParentButton
{
    WorldData _data;
    public void SetInformation(int key)
    {
        _data = WorldDataManager.Instance.GetWorldData(key);
        _text.text = _data.World + " World";
    }
    protected override void OnButtonClick()
    {
       WorldDataManager.Instance.WorldKey = _data.Key;
        GameManager.Instance.PreScene = "LobyScene";
        SceneManager.LoadScene("StageScene");
    }
}
