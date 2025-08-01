using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClickEvent : MonoBehaviour
{
    public void OnClickResetButton()
    {
        CharacterManager.Instance.ResetAll();
    }

    public void OnClickStart()
    {
        //æ¿¿¸»Ø
        GameManager.Instance.PreScene = "StartScene";
        SceneManager.LoadScene("LobyScene");
    }
    public void OnClickWorldButton()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void OnClickGachaButton()
    {
        GameManager.Instance.PreScene = "StageScene";
        SceneManager.LoadScene("CharacterGachaScene");
    }
    public void OnClickExit()
    {
        SceneManager.LoadScene(GameManager.Instance.PreScene);
    }
}
