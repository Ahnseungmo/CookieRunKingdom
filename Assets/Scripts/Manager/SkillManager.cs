using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public SkillButton.Grade GradeStringToEnum(string gradeStr)
    {
        switch (gradeStr.ToLower())
        {
            case "common": return SkillButton.Grade.Common;
            case "rare": return SkillButton.Grade.Rare;
            case "epic": return SkillButton.Grade.Epic;
            case "legendary": return SkillButton.Grade.Legendary;
            default: return SkillButton.Grade.Common;
        }
    }

    // 스킬명 → Type 매핑
    private Dictionary<string, Type> _skillTypeMap = new Dictionary<string, Type>()
    {
        { "WideHeal", typeof(WideHeal) },
        // 필요시 스킬 추가
    };

    // AddComponent 방식으로 Skill 생성
    public Skill CreateSkill(string skillName, Character character)
    {
        if (!_skillTypeMap.TryGetValue(skillName, out var skillType))
        {
            Debug.LogWarning($"SkillManager: {skillName} 스킬 타입 없음");
            return null;
        }

        // 중복 AddComponent 방지
        Skill existingSkill = character.GetComponent(skillType) as Skill;
        if (existingSkill != null)
            return existingSkill;

        // AddComponent로 Skill 붙이기
        Skill skill = character.gameObject.AddComponent(skillType) as Skill;
        // 필요하면 skill.Init(character) 등 추가 세팅
        return skill;
    }
}
