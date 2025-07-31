using Spine;
using Spine.Unity;
using UnityEngine;

public class EnemyAttack : Skill
{
    SkeletonAnimation _animation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        _animation = GetComponent<SkeletonAnimation>();
    }
    public override void Execute()
    {
        print("EnemySkill");
        _animation.AnimationState.SetAnimation(0, "skill1_back", false);
        _animation.AnimationState.Complete -= OnComplete;
        _animation.AnimationState.Complete += OnComplete;
    }
    private void OnComplete(TrackEntry trackEntry)
    {
        Character character = GetComponent<Character>();
        _animation.AnimationState.Complete -= OnComplete;
        character.State = CharState.Idle;
    }
}
