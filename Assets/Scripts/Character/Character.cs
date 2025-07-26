using Spine.Unity;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int key;
    [SerializeField]
    public CharacterAnimations characterAnimations;
    private SkeletonDataAsset _skeletonDataAsset;
    private SkeletonAnimation _skeletonAnimation;
    Spine.AnimationState state;


    private void Awake()
    {
        _skeletonDataAsset = characterAnimations.GetAnimation(key);
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _skeletonAnimation.skeletonDataAsset = _skeletonDataAsset;
        state = _skeletonAnimation.AnimationState;
        //        _skeletonAnimation.s
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state.AddAnimation(0, "idle", true,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
