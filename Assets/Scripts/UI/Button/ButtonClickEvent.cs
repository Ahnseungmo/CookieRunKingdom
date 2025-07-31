using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent: MonoBehaviour
{
    public void OnClickResetButton()
    {
        CharacterManager.Instance.ResetAll();
    }

    public void OnClickSorting()
    {
        //정렬버튼 만들기
    }
}
