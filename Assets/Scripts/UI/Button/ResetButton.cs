using UnityEngine;

public class ResetButton : ParentButton
{
    protected override void OnButtonClick()
    {
        CharacterManager.Instance.ResetAll();
    }
}
