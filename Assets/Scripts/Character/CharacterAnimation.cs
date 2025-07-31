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


        /*
        if (_character.CompareTag("Player"))
        {
            animationKey.Add(CharState.Idle, "battle_idle_back");
            animationKey.Add(CharState.Attack, "skill1_back");
            animationKey.Add(CharState.Move, "run_back");
            animationKey.Add(CharState.Skill, "skill2_loop_back");
            animationKey.Add(CharState.Idle_front, "idle");
            animationKey.Add(CharState.Victory, "joy");
            animationKey.Add(CharState.Lose, "lose");

        }
        if (_character.CompareTag("Enemy"))
        {
            animationKey.Add(CharState.Idle, "battle_idle");
            animationKey.Add(CharState.Attack, "skill1");
            animationKey.Add(CharState.Move, "run");

        }
        */

    }

    private void Init()
    {
        _character = GetComponent<Character>();
        key = _character.CharData.Key;
        print(key);
        CharacterAnimationData data = characterAnimations.GetAnimationData(key);

        animationKey.Add(CharState.Idle, data.idle);
        animationKey.Add(CharState.Skill1, data.skill1);
        animationKey.Add(CharState.Skill2, data.skill2);
        animationKey.Add(CharState.Skill3, data.skill3);
        animationKey.Add(CharState.Run, data.run);
        animationKey.Add(CharState.Victory, data.victory);
        animationKey.Add(CharState.Lose, data.lose);

        animationKey.Add(CharState.Idle_front, "idle");
 
        _skeletonAnimation = GetComponent<SkeletonAnimation>();

        _skeletonDataAsset = characterAnimations.GetAnimation(key);
        _skeletonAnimation.skeletonDataAsset = _skeletonDataAsset;
        _skeletonAnimation.Initialize(true);
        state = _skeletonAnimation.AnimationState;


        if (key < 2000)
        {
            transform.GetComponent<MeshRenderer>().sortingOrder = 1;
        }
        else
        {
            transform.GetComponent<MeshRenderer>().sortingOrder = 0;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _charState = _character.State;
        state.SetAnimation(0, animationKey[CharState.Idle], true);
        //        state.SetAnimation(0, animationKey[_charState], true);

    }

    // Update is called once per frame
    void Update()
    {
        if(key != _character.CharData.Key)
        {
            Init();
            _charState = 0;
        }
//        print(_charState);

        if(_charState != _character.State)
        {
            _charState = _character.State;
            switch (_charState)
            {
                case CharState.Idle:
                case CharState.Victory:
                case CharState.Lose:
                case CharState.Run:
                case CharState.Idle_front:
                    state.SetAnimation(0, animationKey[_charState], true);
                    _isLoop = true;
                    break;
                    /*
                case CharState.Attack:
                case CharState.Skill:

                    state.SetAnimation(0, animationKey[_charState], false);
                    _isLoop = false;
                    state.Complete += OnComplete;
                       
                    if(_charState == CharState.Attack)
                    {
//                        state.Complete += (BattleCharacter)_character.MeleAttack;
                         
                    }

                   
                    break;
                     */
            }

        }


    }
    private void OnComplete(TrackEntry trackEntry)
    {
        state.Complete -= OnComplete;
        _character.State = CharState.Idle;
    }
}


