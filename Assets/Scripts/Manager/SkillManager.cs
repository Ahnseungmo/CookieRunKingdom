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

    private Dictionary<string, Func<Skills>> _skillFactory = new Dictionary<string, Func<Skills>>()
    {
        {"WideHeal", () => new WideHeal()},
        // 필요하면 다른 스킬 추가
    };

    public Skills CreateSkill(string skillName, float coolTime)
    {
        if (_skillFactory.TryGetValue(skillName, out var creator))
        {
            Skills skill = creator();
            skill.Init(skillName, coolTime);
            return skill;
        }
        Debug.LogWarning($"SkillManager: {skillName} 스킬 없음");
        return null;
    }
}
