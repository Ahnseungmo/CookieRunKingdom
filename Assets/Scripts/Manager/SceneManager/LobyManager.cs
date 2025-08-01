using UnityEngine;
using UnityEngine.UI;

public class LobyManager : MonoBehaviour
{
    private PoolingManager _stageButton;
    private Image _panel;
    private bool _isWorldButtonClick = false;
    public void OnClickWorld()
    {
        if (_isWorldButtonClick)
        {
            _panel.gameObject.SetActive(false);
            _isWorldButtonClick = false;
            return;
        }

        _isWorldButtonClick = true;
        _panel.gameObject.SetActive(true);
    }
    private void Start()
    {
        _panel = GameObject.Find("StagePanel/Panel").GetComponent<Image>();
        CreateButtons();
        _panel.gameObject.SetActive(false);
    }
    private void CreateButtons()
    {
        _stageButton = new PoolingManager("Prefabs/Buttons/WorldButton", _panel.gameObject, WorldDataManager.Instance.GetWorldCount());
        for(int i=1;i<=WorldDataManager.Instance.GetWorldCount();i++)
        {
            WorldButton obj = _stageButton.Pop().GetComponent<WorldButton>();
            obj.SetInformation(100 * i);
        }
    }
}
