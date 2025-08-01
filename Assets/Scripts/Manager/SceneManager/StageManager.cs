using UnityEngine;

public class StageManager: MonoBehaviour
{
    private WorldData _worldData;
    private PoolingManager _stageButtons;
    private GameObject _buttonParent;

    private void Start()
    {
        Init(100); // 이거 값 넘겨줘야되는디
    }
    private void Init(int stageNum)
    {
        _buttonParent = GameObject.Find("Buttons");
        _worldData = WorldDataManager.Instance.GetWorldData();
        
        CreateStageButtons(); // 풀링으로 셋팅후에 켜주기 (이때, 정보 넘겨서 정보 셋팅하기)
        ChangeBackGround();
    }

    private void CreateStageButtons()
    {
        _stageButtons = new PoolingManager("Prefabs/Buttons/StageButton", _buttonParent, 10);

        for(int i = 1; i <= _worldData.StageNum; i++)
        {
            StageButton obj = _stageButtons.Pop().GetComponent<StageButton>();
            obj.Data = WorldDataManager.Instance.GetStageData(_worldData.Key + i);
            obj.SetInformation();
        }
    }
    private void ChangeBackGround()
    {
        GameObject bg = GameObject.Find("Background");
        SpriteRenderer bgRenderer = bg.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>(_worldData.BgImage);
        bgRenderer.sprite = sprite;
    }
}
