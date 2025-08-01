using UnityEngine;
using System.Collections.Generic;

public class BattleSceneManager : MonoBehaviour
{
    public GameObject battleCookiePrefab;
    public GameObject battleEnemyPrefab;

    public Vector2 basePosition = new Vector2(-7f, -3f); // 가장 왼쪽 아래 기준점
    public float cellOffsetX = 2.5f; // 칸 가로간격
    public float cellOffsetY = 2.5f; // 칸 세로간격
    void Start()
    {
        DataManager.Instance.LoadAllData();

        int[,] arr = CharacterManager.Instance.CharacterArr; // 3x3 캐릭터 배치 정보

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int key = arr[y, x];
                if (key == 0)
                {
                    continue; // 빈 칸이면 패스
                }
                else if (key == 1)
                {
                    CharacterData charData = DataManager.Instance.GetCharacterData(key);

                    // 각 칸에 맞는 월드 위치 계산
                    Vector3 spawnPos = new Vector3(
                        basePosition.x + x * cellOffsetX,
                        basePosition.y + y * cellOffsetY,
                        0);

                    GameObject go = Instantiate(battleCookiePrefab, spawnPos, Quaternion.identity);
                    BattleCookie cookie = go.GetComponent<BattleCookie>();
                    cookie.CharData = charData;

                    // 실제 등록!
                    CharacterManager.Instance.RegisterCharacter(key, go);

                }
                //else if (charData.Type == 2) // 몬스터(에너미)
                //{
                //    GameObject go = Instantiate(battleEnemyPrefab, monsterStartPos, Quaternion.identity);
                //    BattleEnemy enemy = go.GetComponent<BattleEnemy>();
                //    enemy.CharData = charData;
                //    prefab = battleEnemyPrefab;
                //    spawnPos = monsterStartPos;
                //    moveDir = monsterMoveDir;
                //    monsterStartPos.x -= monsterOffset;
                //}
            }
        }
    }
}
