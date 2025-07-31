using System.Collections.Generic;
using Spine.Unity;
using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character : MonoBehaviour
{
    [SerializeField]
    private CharState _state;
    public CharState State { get { return _state; } set { _state = value; } }
    private float _maxHp;

    [SerializeField]
    private CharacterData _charData;
    public CharacterData CharData { get { return _charData; } set { _charData = value; } }


    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
     

    }

    private void OnEnable()
    {
        _maxHp = CharData.Hp;
    }
    public void Spawn(CharacterData characterData,Vector3 position)
    {
        _charData = characterData;
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void HitDamage(float damage)
    {
        _charData.Hp -= damage;
        if (_charData.Hp < 0)
        {
            _charData.Hp = 0;
            gameObject.gameObject.SetActive(false);
        }
    }
    public void HitHeal(float heal)
    {
        _charData.Hp += heal;
        if (_charData.Hp > _maxHp)
            _charData.Hp = _maxHp;
    }

    public void AttackDamage(Character targer,float damage)
    {
        targer.HitDamage(damage);
    }

    public void HealTarget(Character targer, float heal)
    {
        targer.HitHeal(heal);
    }

  
}
