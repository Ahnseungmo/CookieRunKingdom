using UnityEngine;

public class WideHealSkill : Skills
{
    protected override void UseSkill(CharacterData owner)
    {
        float healAmount = owner.Attack * 0.5f;
        //owner.Heal(healAmount);

        //Debug.Log($"{owner.Name}가 {healAmount}만큼 회복! (최종체력: {runtimeOwner.CurHp}/{owner.Hp})");

        //임시코드
        owner.Hp += healAmount;
        Debug.Log("아군이 {healAmount} 만큼 회복");
    }
}

