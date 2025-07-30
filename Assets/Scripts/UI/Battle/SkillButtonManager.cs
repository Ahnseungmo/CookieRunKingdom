using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonManager : MonoBehaviour
{
    public GameObject skillButtonPrefab;
    public Transform skillButtonsParent;

    void Start()
    {
        DataManager.Instance.LoadAllData();

        // ★ 전체 캐릭터 데이터 기준으로 버튼 생성 ★
        List<CharacterData> charList = DataManager.Instance.GetAllCharacterData();
        for (int i = 0; i < charList.Count; ++i)
        {
            CharacterData charData = charList[i];

            // 빈 슬롯 체크(필요시, 아니면 생략 가능)
            if (charData.Key <= 0) continue;

            GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonsParent);
            SkillButton btn = btnObj.GetComponent<SkillButton>();

            // 등급별 UI 세팅
            SkillButton.Grade gradeEnum = SkillManager.Instance.GradeStringToEnum(charData.Grade);
            btn.SetGrade(gradeEnum);
            btn.StartCooldown(0);

            // 스킬 인스턴스 생성 (팩토리+초기화)
            Skills skill = SkillManager.Instance.CreateSkill(charData.SkillName, charData.Cooltime);

            // 클로저 복사
            CharacterData capturedCharData = charData;
            SkillButton capturedBtn = btn;
            Skills capturedSkill = skill;

            Button uiButton = btnObj.GetComponent<Button>();
            uiButton.onClick.AddListener(() =>
            {
                capturedBtn.StartCooldown(capturedCharData.Cooltime);

                if (capturedSkill != null)
                    capturedSkill.CheckSkill(capturedCharData);
                else
                    Debug.LogWarning($"스킬 없음: {capturedCharData.SkillName}");
            });
        }
    }
}


//{
//    public GameObject skillButtonPrefab;
//    public Transform skillButtonsParent;
//
//    void Start()
//    {
//        // 데이터 로드
//        DataManager.Instance.LoadAllData();
//
//        // 파티에 배치된 캐릭터 키 리스트 (CharacterManager에서 가져옴)
//        List<int> charKeyList = CharacterManager.Instance.Character;
//        for (int i = 0; i < charKeyList.Count; ++i)
//        {
//            int charKey = charKeyList[i];
//            if (charKey <= 0) continue; // 빈 슬롯 스킵
//
//            CharacterData charData = DataManager.Instance.GetCharacterData(charKey);
//
//            // 버튼 프리팹 생성
//            GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonsParent);
//            SkillButton btn = btnObj.GetComponent<SkillButton>();
//
//            // 등급별 UI 세팅
//            SkillButton.Grade gradeEnum = SkillManager.Instance.GradeStringToEnum(charData.Grade);
//            btn.SetGrade(gradeEnum);
//            btn.StartCooldown(0);
//
//            // 각 캐릭터의 스킬 생성 (팩토리 + 초기화)
//            Skills skill = SkillManager.Instance.CreateSkill(charData.SkillName, charData.Cooltime);
//
//            // 클릭 이벤트 (람다 클로저 캡처! 반드시 복사본)
//            CharacterData capturedCharData = charData;
//            SkillButton capturedBtn = btn;
//            Skills capturedSkill = skill;
//
//            Button uiButton = btnObj.GetComponent<Button>();
//            uiButton.onClick.AddListener(() =>
//            {
//                // 쿨타임 UI
//                capturedBtn.StartCooldown(capturedCharData.Cooltime);
//
//                // 쿨타임/조건 체크 & 스킬 실행
//                if (capturedSkill != null)
//                    capturedSkill.CheckSkill(capturedCharData);
//                else
//                    Debug.LogWarning($"스킬 없음: {capturedCharData.SkillName}");
//            });
//        }
//    }
//}
