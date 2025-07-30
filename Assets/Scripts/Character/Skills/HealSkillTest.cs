using UnityEngine;

public class HealSkillTest : MonoBehaviour
{
    public float maxHp = 100;
    public float curHp = 60;
    public float attack = 40;

    private WideHeal healSkill;

    void Start()
    {
        // 임시 캐릭터 데이터 생성
        CharacterData data = new CharacterData
        {
            Name = "임시쿠키",
            Hp = maxHp,
            Attack = attack,
        };

        healSkill = new WideHeal();
        healSkill.Init("힐스킬", 5.0f);

        // 처음 상태 출력
        Debug.Log($"[초기] {data.Name} 현재체력: {curHp}/{data.Hp}");
    }

    [ContextMenu("힐 테스트!")]
    public void TestHeal()
    {
        // 임시 캐릭터 데이터 생성
        CharacterData data = new CharacterData
        {
            Name = "임시쿠키",
            Hp = maxHp,
            Attack = attack,
        };

        // 힐 적용
        float healAmount = data.Attack * 0.5f;
        curHp += healAmount;
        if (curHp > data.Hp)
            curHp = data.Hp;

        Debug.Log($"힐 실행! {data.Name}이(가) {healAmount}만큼 회복! (최종: {curHp}/{data.Hp})");
    }
}
