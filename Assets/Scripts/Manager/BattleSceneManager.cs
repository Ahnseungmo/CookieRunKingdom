using UnityEngine;
using System.Collections.Generic;

public class BattleSceneManager : MonoBehaviour
{
    public GameObject battleCookiePrefab;   // 플레이어(쿠키) 프리팹
    public GameObject battleEnemyPrefab;    // 몬스터(에너미) 프리팹

    void Start()
    {
        DataManager.Instance.LoadAllData();

        List<CharacterData> charList = DataManager.Instance.GetAllCharacterData();

        Vector3 playerStartPos = new Vector3(-7f, -3f, 0);
        Vector3 monsterStartPos = new Vector3(7f, 3f, 0);

        Vector3 playerMoveDir = new Vector3(1f, 0.5f, 0).normalized;
        Vector3 monsterMoveDir = new Vector3(-1f, -0.5f, 0).normalized;

        float playerOffset = 1.5f;
        float monsterOffset = 1.5f;

        for (int i = 0; i < charList.Count; i++)
        {
            CharacterData charData = charList[i];
            GameObject prefab = null;
            Vector3 spawnPos = Vector3.zero;
            Vector3 moveDir = Vector3.zero;

            if (charData.Type == 1) // 플레이어(쿠키)
            {
                GameObject go = Instantiate(battleCookiePrefab, playerStartPos, Quaternion.identity);
                BattleCookie cookie = go.GetComponent<BattleCookie>();
                cookie.CharData = charData;
                prefab = battleCookiePrefab;
                spawnPos = playerStartPos;
                moveDir = playerMoveDir;
                playerStartPos.x += playerOffset;
                cookie.Spawn(charData, spawnPos);
            }
            else if (charData.Type == 2) // 몬스터(에너미)
            {
                GameObject go = Instantiate(battleEnemyPrefab, monsterStartPos, Quaternion.identity);
                BattleEnemy enemy = go.GetComponent<BattleEnemy>();
                enemy.CharData = charData;
                prefab = battleEnemyPrefab;
                spawnPos = monsterStartPos;
                moveDir = monsterMoveDir;
                monsterStartPos.x -= monsterOffset;
            }
        }
    }
}
