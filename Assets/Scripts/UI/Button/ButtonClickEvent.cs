using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClickEvent: MonoBehaviour
{
    public void OnClickResetButton()
    {
        CharacterManager.Instance.ResetAll();
    }

    public void OnClickStart()
    {
        //æ¿¿¸»Ø
        SceneManager.LoadScene("LobyScene");
    }
    public void OnClickWorldButton()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void OnClickGachaButton()
    {
        SceneManager.LoadScene("CharacterGachaScene");
    }
}
