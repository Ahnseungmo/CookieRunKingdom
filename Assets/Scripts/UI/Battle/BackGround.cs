using UnityEngine;

public class Background : MonoBehaviour
{
    public Camera cam;
    public GameObject roadTile;         // 중앙 도로
    public GameObject roadEdgeTile;     // 도로 경계(풀/그림자)
    public GameObject grassTile;        // 숲/풀 타일

    public Vector2Int tileCount = new Vector2Int(7, 7); // 더 넓게(홀수 추천)
    public Vector2 tileSize;

    private GameObject[,] tiles;
    private Vector2Int centerIdx;
    private Vector2Int lastCamTile;

    void Start()
    {
        // roadTile 기준으로 tileSize 자동 설정
        if (roadTile != null)
            tileSize = roadTile.GetComponent<SpriteRenderer>().bounds.size;

        centerIdx = new Vector2Int(tileCount.x / 2, tileCount.y / 2);
        tiles = new GameObject[tileCount.x, tileCount.y];

        for (int y = 0; y < tileCount.y; y++)
        {
            for (int x = 0; x < tileCount.x; x++)
            {
                int mapX = x - centerIdx.x;
                int mapY = y - centerIdx.y;
                GameObject prefab = GetTilePrefab(x, y);

                tiles[x, y] = Instantiate(prefab, transform);
                Vector3 pos = IsoTileToWorld(mapX, mapY);
                tiles[x, y].transform.position = pos;
            }
        }

        lastCamTile = GetCamTilePos(cam.transform.position);
        UpdateTiles(cam.transform.position);
    }

    void Update()
    {
        Vector3 camPos = cam.transform.position;
        Vector2Int camTile = GetCamTilePos(camPos);

        if (camTile != lastCamTile)
        {
            UpdateTiles(camPos);
            lastCamTile = camTile;
        }
    }

    Vector2Int GetCamTilePos(Vector3 camPos)
    {
        int x = Mathf.FloorToInt(camPos.x / (tileSize.x / 2f));
        int y = Mathf.FloorToInt(camPos.y / (tileSize.y / 2f));
        return new Vector2Int(x, y);
    }

    void UpdateTiles(Vector3 camPos)
    {
        Vector2Int camTile = GetCamTilePos(camPos);

        for (int y = 0; y < tileCount.y; y++)
        {
            for (int x = 0; x < tileCount.x; x++)
            {
                int mapX = camTile.x + (x - centerIdx.x);
                int mapY = camTile.y + (y - centerIdx.y);

                Vector3 pos = IsoTileToWorld(mapX, mapY);
                tiles[x, y].transform.position = pos;
            }
        }
    }

    Vector3 IsoTileToWorld(int x, int y)
    {
        float wx = (x + y) * (tileSize.x / 2f);
        float wy = (y - x) * (tileSize.y / 2f);
        return new Vector3(wx, wy, 0);
    }

    // 도로 중앙 1줄 + 양옆 경계, 나머지 숲
    GameObject GetTilePrefab(int x, int y)
    {
        // 중앙 1줄(예: 세로), 중앙 line을 roadTile로
        if (x == centerIdx.x)
            return roadTile;
        // 중앙 양옆만 경계 풀 (필요시 더 넓게)
        else if (Mathf.Abs(x - centerIdx.x) == 1)
            return roadEdgeTile;
        else
            return grassTile;
    }
}
