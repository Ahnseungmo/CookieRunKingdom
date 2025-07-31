using Spine.Unity;
using Spine;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected Character _character;
    protected CharacterAnimations _characterAnimations;
    protected SkeletonAnimation _animation;
    private void OnEnable()
    {
        ReadData();
    }

    protected void ReadData()
    {
        _animation = GetComponent<SkeletonAnimation>();
        _character = GetComponent<Character>();
        _characterAnimations = Resources.Load<CharacterAnimations>("Tables/CharacterAnimations");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    abstract public void Execute();
}
