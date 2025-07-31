using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleEnemy : Character
{
    GameObject _basePos;
    float _speed = 1.0f;
    float _range;
    private readonly float AttackTime = 3.0f;
    private float _attackTimer = 0.0f;
    private Skill _skill1;
    private Skill _skill2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnEnable()
    {

        if (CharData.Type == 1)
        {
            _range = 1.0f;
        }
        if (CharData.Type == 2)
        {
            _range = 3.0f;
        }
        _skill1 = (Skill)gameObject.AddComponent(DataManager.skillMap[CharData.Skill1]);
        _speed = CharData.Speed;
    }

    // Update is called once per frame
    void Update()
    {


        if(State == CharState.Move || State == CharState.Idle)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _range);

            bool findTarget = false;
            foreach (Collider2D target in targets)
            {
                if (target.CompareTag("Player"))
                {
                    findTarget = true;
                    break;
                }
            }
            if (findTarget) State = CharState.Idle;
            else State = CharState.Move;
        }

        switch (State)
        {
            case CharState.Idle:
                Idle();
                break;
            case CharState.Move:
                Move();
                break;
        
        }

    }
    private void Move()
    {
        Vector3 movedir = (new Vector3(1, 0.5f, 0)).normalized;
        transform.Translate(-movedir * _speed * Time.deltaTime);

        if (_basePos != null)
        {
            Vector3 dir = (_basePos.transform.position - transform.position).normalized;
            transform.Translate(dir * _speed * Time.deltaTime);
        }
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
                Skill1();
            }
        }
    }
    private void Skill1()
    {
        //print("skill1");
        State = CharState.Attack;
        _skill1.Execute();
    }

    public void Spawn(CharacterData characterData, Vector3 position, GameObject basePos = null)
    {
        base.Spawn(characterData, position);
        if (basePos != null)
            _basePos = basePos;


    }
}