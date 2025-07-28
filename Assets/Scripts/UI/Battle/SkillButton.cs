using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillButton : MonoBehaviour
{
    public enum Grade { Common, Rare, Epic, Legendary }

    private Image bgImage;
    
    private Button skillButton;
    private Image hpBarImage;
    private Sprite[] backgroundSprites;


    void Awake()
    {
        bgImage = GetComponent<Image>();
        skillButton = GetComponent<Button>();

        backgroundSprites = new Sprite[4];
        backgroundSprites[0] = Resources.Load<Sprite>("Textures/BattleScene/Common");
        backgroundSprites[1] = Resources.Load<Sprite>("Textures/BattleScene/Rare");
        backgroundSprites[2] = Resources.Load<Sprite>("Textures/BattleScene/Epic");
        backgroundSprites[3] = Resources.Load<Sprite>("Textures/BattleScene/Legendary");
        hpBarImage = transform.Find("HPBar").GetComponent<Image>();
    }

    public void SetGrade(Grade grade)
    {
        if (backgroundSprites != null && (int)grade < backgroundSprites.Length)
            bgImage.sprite = backgroundSprites[(int)grade];
    }

    public void SetButtonEvent(Action onClick)
    {
        skillButton.onClick.RemoveAllListeners();
        skillButton.onClick.AddListener(() => onClick());
    }
}
