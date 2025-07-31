using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAnimationData
{
    public int key;
    public SkeletonDataAsset skeletonDataAsset;
    public string idle;
    public string run;
    public string victory;
    public string lose;
    public string skill1;
    public string skill2;
    public string skill3;
}

[CreateAssetMenu(fileName = "CharacterAnimations", menuName = "Scriptable Objects/CharacterAnimations")]
public class CharacterAnimations : ScriptableObject
{
    [SerializeField]
    private List<CharacterAnimationData> animationList = new List<CharacterAnimationData>();

    private Dictionary<int, CharacterAnimationData> _animationDict;

    public void Init()
    {
        if (_animationDict == null)
        {
            _animationDict = new Dictionary<int, CharacterAnimationData>();
            foreach (var data in animationList)
            {
                if (!_animationDict.ContainsKey(data.key))
                    _animationDict.Add(data.key, data);
            }
        }
    }

    public SkeletonDataAsset GetAnimation(int key)
    {
        if (_animationDict == null) Init();
        _animationDict.TryGetValue(key, out var anim);
        return anim.skeletonDataAsset;
    }
    public CharacterAnimationData GetAnimationData(int key)
    {
        if (_animationDict == null) Init();
        _animationDict.TryGetValue(key, out var animData);
        return animData;
    }
}