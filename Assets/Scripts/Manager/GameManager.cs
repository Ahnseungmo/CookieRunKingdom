using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private string _preScene;
    private bool _isWin = false;
    public bool IsWin
    {
        get { return _isWin; }
        set { _isWin = value; }
    }
    public string PreScene
    {
        get { return _preScene; }
        set { _preScene = value; }
    }
    //스테이지 시작하면 스테이지매니저 부르기
    private void Awake()
    {
        DataManager.Instance.LoadAllData();
        InventoryManager.Instance.SetData();
        WorldDataManager.Instance.SetData();
    }
}
