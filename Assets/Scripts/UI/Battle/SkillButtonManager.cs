using UnityEngine;
using UnityEngine.UI;

public class SkillButtonManager : MonoBehaviour
{
    public GameObject skillButtonPrefab;
    public Transform skillButtonsParent;

    void Start()
    {
        //임시로 다받아옴 배치된 캐릭터 정보 받아와야함
        DataManager.Instance.LoadAllData();
        var charList = DataManager.Instance.GetAllCharacterData();
        for (int i = 0; i < Mathf.Min(5, charList.Count); ++i)
        {
            CharacterData charData = charList[i];
            GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonsParent);
            SkillButton btn = btnObj.GetComponent<SkillButton>();

            // 1. 등급(enum) 변환 및 등급별 이미지 적용
            SkillButton.Grade gradeEnum = SkillManager.Instance.GradeStringToEnum(charData.Grade);
            btn.SetGrade(gradeEnum);

            // 2. 쿨타임(초) 설정
            btn.StartCooldown(0); // 초기엔 쿨타임 없음(혹은 비활성)으로 시작, 실제 사용시 쿨타임 적용

            // 3. 버튼 클릭 이벤트 연결 (반드시 Button 컴포넌트의 OnClick에 연결!)
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                // 3-1. 쿨타임 시작
                btn.StartCooldown(charData.Cooltime);

                // 3-2. (옵션) 실제 스킬 효과, 로그
                float curHp = 60;
                float healAmount = charData.Attack * 0.5f;
                curHp += healAmount;
                if (curHp > charData.Hp)
                    curHp = charData.Hp;

                Debug.Log($"{charData.Name} 스킬 사용! {healAmount}만큼 회복 (최종: {curHp}/{charData.Hp})");
            });
        }
    }
}
