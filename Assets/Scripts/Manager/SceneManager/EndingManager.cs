using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    private Canvas _winPanel;
    private Canvas _losePanel;

    public void OnClickExit()
    {
        SceneManager.LoadScene("LobyScene");
    }
    public void OnClickNextStage()
    {
        WorldDataManager.Instance.StageKey = WorldDataManager.Instance.StageKey+1;
        SceneManager.LoadScene("GamePlayScene");
    }
    private void Start()
    {
        _winPanel = GameObject.Find("WinPanel").GetComponent<Canvas>();
        _losePanel = GameObject.Find("LosePanel").GetComponent<Canvas>();

        _winPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);
        if (GameManager.Instance.IsWin)
        {
            _winPanel.gameObject.SetActive(true);
        }
        else
            _losePanel.gameObject.SetActive(true);
    }


}
