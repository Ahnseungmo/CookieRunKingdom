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
}
