using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;

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

    public int stageKey = 101; // 현재 스테이지(임의값, 필요시 변수로)

    private bool _isEnd = false;
    private float _timer = 0.0f;
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

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer>60.0f)
        {
            SceneManager.LoadScene("GameEndingScene");
        }
    }
    // 2차원 몬스터 진형 배치 (한줄에 3마리씩, 2~3줄)
    void SpawnMonsterGrid(int stageKey)
    {
        StageData stageData = DataManager.Instance.GetStageData(stageKey);

        // 웨이브별 마릿수 배열
        int[] waveCounts = new int[] { stageData.Wave1, stageData.Wave2, stageData.Wave3 };

        int monsterKey = 2001; // 하드코딩 몬스터 key (예시)

        int spawnIdx = 0;
        int colCount = 3; // 한 줄에 3마리씩
        for (int wave = 0; wave < waveCounts.Length; wave++)
        {
            int count = waveCounts[wave];
            for (int i = 0; i < count; i++)
            {
                Debug.Log($"[몬스터 소환] 웨이브{wave + 1} / idx={i} / key=2001");
                int row = spawnIdx / colCount;
                int col = spawnIdx % colCount;

                CharacterData monsterData = DataManager.Instance.GetCharacterData(monsterKey);

                Vector3 spawnPos = new Vector3(
                    enemyBasePosition.x + col * enemyCellOffsetX,
                    enemyBasePosition.y + row * enemyCellOffsetY,
                    0);

                GameObject go = Instantiate(battleEnemyPrefab, spawnPos, Quaternion.identity);
                BattleEnemy enemy = go.GetComponent<BattleEnemy>();
                enemy.CharData = monsterData;

                spawnIdx++;
            }
        }
    }

}
