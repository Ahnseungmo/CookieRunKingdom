using UnityEngine;

public class Skills
{
    protected float _coolTime;
    protected string _skillName;
    protected float _lastUseTime;
    public virtual void Init(string skillName, float coolTime)
    {
        _skillName = skillName;
        _coolTime = coolTime;
        _lastUseTime = -999f;
    }
    public bool CanUseSkill()
    {
        return Time.time >= _lastUseTime + _coolTime;
    }
    public void CheckSkill(CharacterData owner)
    {
        if (CanUseSkill())
        {
            UseSkill(owner);
            _lastUseTime = Time.time;
        }
        else
        {
            Debug.Log($"{_skillName} 쿨타임 남음!");
        }
    }
    protected virtual void UseSkill(CharacterData owner)
    {

    }
}
