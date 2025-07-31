using Spine.Unity;
using Spine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject _target;
    float _speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Spawn(GameObject spawner,GameObject target)
    {
        transform.position = spawner.transform.position;
        _target = target;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            gameObject.SetActive(false);
            return;
        }
        float distacne = Vector3.Distance(transform.position,_target.transform.position);
        if (distacne < 0.5)
        {
            gameObject.SetActive(false);
            return;

        }
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir * _speed * Time.deltaTime);
    }
    
}
