using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseAttack : Skill
{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public override void Execute()
    {

        print("BaseSkill");
        int key = _character.CharData.Key;
        CharacterAnimationData data = _characterAnimations.GetAnimationData(key);
        string clip = data.skill1;

        _animation.AnimationState.SetAnimation(0, clip, false);
        _animation.AnimationState.Complete -= OnComplete;
        _animation.AnimationState.Complete += OnComplete;
    }
    private void OnComplete(TrackEntry trackEntry)
    {
        Character character = GetComponent<Character>();
        _animation.AnimationState.Complete -= OnComplete;
        character.State = CharState.Idle;


        GameObject target;

//        target.GetComponent<Character>().HitDamage(10);
    }


}
