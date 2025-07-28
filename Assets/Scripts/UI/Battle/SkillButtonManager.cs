using UnityEngine;

public class SkillButtonManager : MonoBehaviour
{
    void Start()
    {
        // 1. 프리팹 Resources에서 로드 (Resources/Prefabs/SkillButton)
        GameObject skillButtonPrefab = Resources.Load<GameObject>("Prefabs/SkillButton");
        if (skillButtonPrefab == null)
        {
            return;
        }

        // 2. SkillButtons 부모 오브젝트 찾기
        GameObject skillButtonsParentObj = GameObject.Find("SkillButtons");
        if (skillButtonsParentObj == null)
        {
            return;
        }
        Transform skillButtonsParent = skillButtonsParentObj.transform;

        // 3. 버튼 5개 생성해서 등급별 배경 적용
        for (int i = 0; i < 5; ++i)
        {
            GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonsParent);
            SkillButton btn = btnObj.GetComponent<SkillButton>();
            // 0: Common, 1: Rare, 2: Epic, 3: Legendary, 4: Common 반복
            btn.SetGrade((SkillButton.Grade)(i % 4));
        }
    }
}
