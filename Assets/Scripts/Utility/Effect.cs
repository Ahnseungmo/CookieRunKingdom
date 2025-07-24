using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator _animator;
    private AnimatorStateInfo _animatorStateInfo;

    private void Awake()
    {
        _animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_animatorStateInfo.normalizedTime >= 1.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
