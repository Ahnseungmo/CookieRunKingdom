using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonManager : MonoBehaviour
{
    public GameObject skillButtonPrefab;
    public Transform skillButtonsParent;

    void Start()
    {
        // 1. 선택된 캐릭터 Key들
        var selectedKeys = CharacterManager.Instance.Character;

        // 2. 전체 캐릭터 데이터에서 선택된 캐릭터만
        List<CharacterData> allCharacters = DataManager.Instance.GetAllCharacterData();
        foreach (var charData in allCharacters)
        {
            if (charData.Key <= 0) continue;
            if (!selectedKeys.Contains(charData.Key)) continue;

            GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonsParent);
            SkillButton btn = btnObj.GetComponent<SkillButton>();
            SkillButton.Grade gradeEnum = SkillManager.Instance.GradeStringToEnum(charData.Grade);
            btn.SetGrade(gradeEnum);
            btn.StartCooldown(0);
            
            GameObject charObj = CharacterManager.Instance.GetCharacterObject(charData.Key);
            Character character = charObj.GetComponent<Character>();

            Skill skill = SkillManager.Instance.CreateSkill(charData.SkillName, character);

            Button uiButton = btnObj.GetComponent<Button>();
            uiButton.onClick.AddListener(() =>
            {
                btn.StartCooldown(charData.Cooltime);
                if (skill != null)
                    skill.Execute();
                else
                    Debug.LogWarning($"스킬 없음: {charData.SkillName}");
            });
        }
    }

}
