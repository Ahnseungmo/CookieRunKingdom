using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private int key;
    [SerializeField]
    public CharacterAnimations characterAnimations;
    private SkeletonDataAsset _skeletonDataAsset;
    private SkeletonAnimation _skeletonAnimation;
    Spine.AnimationState state;
    Dictionary<CharState,string> animationKey = new Dictionary<CharState, string>();
    private CharState _charState;
    private Character _character;

    bool _isLoop = false;
    private void Awake()
    {
        Init();
        if (_character.CompareTag("Player"))
        {
            animationKey.Add(CharState.Idle, "idle_back");
            animationKey.Add(CharState.Attack, "skill1_back");
            animationKey.Add(CharState.Move, "run_back");
            animationKey.Add(CharState.Skill, "skill2_loop_back");
            animationKey.Add(CharState.Idle_front, "idle");
            animationKey.Add(CharState.Victory, "joy");
            animationKey.Add(CharState.Lose, "lose");
        }
    }

    private void Init()
    {
       _character = GetComponent<Character>();
        key = _character.CharData.Key;
        print(key);

        _skeletonAnimation = GetComponent<SkeletonAnimation>();


        _skeletonDataAsset = characterAnimations.GetAnimation(key);
        _skeletonAnimation.skeletonDataAsset = _skeletonDataAsset;
        _skeletonAnimation.Initialize(true);
        state = _skeletonAnimation.AnimationState;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _charState = _character.State;
        state.SetAnimation(0, animationKey[_charState], true);
    }

    // Update is called once per frame
    void Update()
    {
        if(key != _character.CharData.Key)
        {
            Init();
            _charState = 0;
        }
        print(_charState);

        if(_charState != _character.State)
        {
            _charState = _character.State;
            switch (_charState)
            {
                case CharState.Idle:
                case CharState.Victory:
                case CharState.Lose:
                case CharState.Move:
                case CharState.Idle_front:
                    state.SetAnimation(0, animationKey[_charState], true);
                    _isLoop = true;
                    break;

                case CharState.Attack:
                case CharState.Skill:

                    state.SetAnimation(0, animationKey[_charState], false);
                    _isLoop = false;
                    state.Complete += OnComplete;
                       
                    break;

            }

        }


    }
    private void OnComplete(TrackEntry trackEntry)
    {
        state.Complete -= OnComplete;
        _character.State = CharState.Idle;
    }
}


