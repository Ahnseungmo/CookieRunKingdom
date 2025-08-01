using UnityEngine;
using System.Collections.Generic;

public class BattleSceneManager : MonoBehaviour
{
    public GameObject battleCookiePrefab;
    public GameObject battleEnemyPrefab;

    // 쿠키 기준 위치/간격
    public Vector2 cookieBasePosition = new Vector2(-7f, -3f); // 왼쪽 아래
    public float cookieCellOffsetX = 2.5f;
    public float cookieCellOffsetY = 2.5f;

    // 몬스터 기준 위치/간격
    public Vector2 enemyBasePosition = new Vector2(7f, 3f); // 오른쪽 위
    public float enemyCellOffsetX = -2.5f;  // 왼쪽으로 이동(음수)
    public float enemyCellOffsetY = -2.5f;  // 아래로 이동(음수)

    public int stageKey = 1; // 현재 스테이지(임의값, 필요시 변수로)

    void Start()
    {
        DataManager.Instance.LoadAllData();

        // 1. 쿠키(플레이어) 3x3 진형 배치
        int[,] arr = CharacterManager.Instance.CharacterArr;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int key = arr[y, x];
                if (key == 0) continue;

                CharacterData charData = DataManager.Instance.GetCharacterData(key);

                Vector3 spawnPos = new Vector3(
                    cookieBasePosition.x + x * cookieCellOffsetX,
                    cookieBasePosition.y + y * cookieCellOffsetY,
                    0);

                GameObject go = Instantiate(battleCookiePrefab, spawnPos, Quaternion.identity);
                BattleCookie cookie = go.GetComponent<BattleCookie>();
                cookie.CharData = charData;

                CharacterManager.Instance.RegisterCharacter(key, go);
            }
        }

        // 2. 몬스터(StageData 기반, 2차원 진형)
        SpawnMonsterGrid(stageKey);
    }

    // 2차원 몬스터 진형 배치 (한줄에 3마리씩, 2~3줄)
    void SpawnMonsterGrid(int stageKey)
    {
        StageData stageData = DataManager.Instance.GetStageData(stageKey);

        // StageData에서 Wave 정보를 리스트로 추출
        List<int> monsterKeys = new List<int>();
        if (stageData.Wave1 != 0) monsterKeys.Add(stageData.Wave1);
        if (stageData.Wave2 != 0) monsterKeys.Add(stageData.Wave2);
        if (stageData.Wave3 != 0) monsterKeys.Add(stageData.Wave3);

        int rowCount = 2; // 몬스터 행 수(2~3줄)
        int colCount = 3; // 몬스터 한 줄에 3마리

        for (int i = 0; i < monsterKeys.Count; i++)
        {
            int row = i / colCount;
            int col = i % colCount;
            int monsterKey = monsterKeys[i];

            CharacterData monsterData = DataManager.Instance.GetCharacterData(monsterKey);

            Vector3 spawnPos = new Vector3(
                enemyBasePosition.x + col * enemyCellOffsetX,
                enemyBasePosition.y + row * enemyCellOffsetY,
                0);

            GameObject go = Instantiate(battleEnemyPrefab, spawnPos, Quaternion.identity);
            BattleEnemy enemy = go.GetComponent<BattleEnemy>();
            enemy.CharData = monsterData;

            // 필요시 몬스터매니저에 Register도 가능
        }
    }
}
