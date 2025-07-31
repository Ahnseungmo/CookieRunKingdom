using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent: MonoBehaviour
{
    public void OnClickResetButton()
    {
        CharacterManager.Instance.ResetAll();
    }

    public void OnClickStart()
    {
        //æ¿¿¸»Ø
    }
}
