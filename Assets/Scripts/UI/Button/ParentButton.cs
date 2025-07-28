using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ParentButton : MonoBehaviour
{
    protected TextMeshProUGUI _text;
    protected Image _image;
    protected Button _button;

    private void Awake()
    {
        _text = transform.GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
}
