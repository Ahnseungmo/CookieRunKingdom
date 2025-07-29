using System.Collections.Generic;
using Spine.Unity;
using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleCharacter : Character
{

    private float _attackTimer = 0;
    private readonly float AttackTime = 1.0f;
    private CharacterData _charData;
    private Vector3 basePos;


    float range;
    float speed = 1.0f;


    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_charData.Type == 0) range = 1.0f;
        else if (_charData.Type == 1) range = 3.0f;

    }
    void Spawn(GameObject basePosition)
    {
        basePos = basePosition.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        print(State);
        if (State == CharState.Attack || State == CharState.Skill) return;

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                Idle();
                return;
            }
        }
        Move();


    }

    private void Attack()
    {
        State = CharState.Attack;

    }
    private void Move()
    {
        State = CharState.Move;

        Vector3 direction = (basePos - transform.position).normalized;
        transform.Translate(direction * speed);
//        transform.transform

    }
    private void Idle()
    {
        if (State != CharState.Idle) _attackTimer = 0;
        State = CharState.Idle;
        if (State == CharState.Idle)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > AttackTime)
            {
                _attackTimer -= AttackTime;
                Attack();
            }
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.0f);
        Gizmos.DrawWireSphere(transform.position, 3.0f);
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }
}
