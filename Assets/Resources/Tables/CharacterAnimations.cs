using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAnimationData
{
    public int key;
    public SkeletonDataAsset skeletonDataAsset;
}

[CreateAssetMenu(fileName = "CharacterAnimations", menuName = "Scriptable Objects/CharacterAnimations")]
public class CharacterAnimations : ScriptableObject
{
    [SerializeField]
    private List<CharacterAnimationData> animationList = new List<CharacterAnimationData>();

    private Dictionary<int, SkeletonDataAsset> _animationDict;

    public void Init()
    {
        _animationDict = new Dictionary<int, SkeletonDataAsset>();
        foreach (var data in animationList)
        {
            if (!_animationDict.ContainsKey(data.key))
                _animationDict.Add(data.key, data.skeletonDataAsset);
        }
    }

    public SkeletonDataAsset GetAnimation(int key)
    {
        if (_animationDict == null) Init();
        _animationDict.TryGetValue(key, out var anim);
        return anim;
    }
}
