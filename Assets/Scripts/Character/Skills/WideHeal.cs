using UnityEngine;
using System.Collections.Generic;
public class WideHeal : Skills
{
    protected override void UseSkill(CharacterData owner)
    {
        //임시
        List<CharacterData> charList = DataManager.Instance.GetAllCharacterData();
        for (int i = 0; i < charList.Count; ++i)
        {
            CharacterData charData = charList[i];
            if (charData.Key <= 0) continue;

            float healAmount = charData.Attack * 0.5f;
            charData.Hp += healAmount;

            Debug.Log($"{charData.Name}가 {healAmount}만큼 회복! (최종체력: {charData.Hp})");
        }

    }
}
// 파티에 들어있는 모든 캐릭터 순회
//List<int> keyList = CharacterManager.Instance.Character;
//for (int i = 0; i < keyList.Count; ++i)
//{
//    int charKey = keyList[i];
//    if (charKey <= 0) continue; // 빈칸 스킵
//
//    CharacterData charData = DataManager.Instance.GetCharacterData(charKey);
//
//    float healAmount = charData.Attack * 0.5f; // 각 캐릭터의 공격력 기반 힐
//    charData.Hp += healAmount;
//
//    Debug.Log($"{charData.Name}가 {healAmount}만큼 회복! (최종체력: {charData.Hp})");
//}

//임시코드
//owner.Hp += healAmount;
//Debug.Log("아군이 {healAmount} 만큼 회복");