using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float _moveDamping = 1.0f;

    [SerializeField]
    private Transform _target;

    private void Start()
    {
//        _target = GameManager.Instance.Player.transform;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;
        
        Vector3 targetPosition = _target.position;
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, _moveDamping * Time.deltaTime);
    }
}
