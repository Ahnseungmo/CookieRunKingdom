using UnityEngine;
using UnityEngine.UI;

public class PauseResumeManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Button btnPause;
    public Button btnResume;
    public Button btnClose;
    private void Awake()
    {
        GameObject battleUI = GameObject.Find("BattleUI");
        pausePanel = battleUI.transform.Find("PausePanel").gameObject;
        btnPause = GameObject.Find("BtnPause").GetComponent<Button>();
        btnResume = pausePanel.transform.Find("BtnResume").GetComponent<Button>();
        btnClose = pausePanel.transform.Find("BtnClose").GetComponent<Button>();
        Debug.Log($"btnPause: {btnPause}, btnResume: {btnResume}, btnClose: {btnClose}");


        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        btnClose.onClick.AddListener(ResumeGame);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 정지
        Debug.Log("Game Paused");
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 재개
        Debug.Log("Game Resumed");
        pausePanel.SetActive(false);
    }
}
