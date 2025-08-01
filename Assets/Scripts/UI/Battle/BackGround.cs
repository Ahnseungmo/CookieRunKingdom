using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform[] tilemaps;
    public Camera mainCamera;
    public float tileSizeX = 1f;
    public float tileSizeY = 0.5f;
    public int mapWidth = 86;
    public float moveSpeed = 2f;

    private Vector3 moveDir;
    private Vector3 loopOffset;

    void Start()
    {
        moveDir = new Vector3(tileSizeX, tileSizeY, 0).normalized;
        loopOffset = new Vector3(tileSizeX * mapWidth, tileSizeY * mapWidth, 0);

        tilemaps[0].position = Vector3.zero;
        tilemaps[1].position = loopOffset;

        if (mainCamera != null)
            mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
    }

    void Update()
    {
        Vector3 delta = moveDir * moveSpeed * Time.deltaTime;

        // 1. 맨 앞줄(0번째 row)에서 제일 왼쪽부터 캐릭터 찾기
        int[,] arr = CharacterManager.Instance.CharacterArr;
        int leaderKey = 0;
        for (int x = 0; x < arr.GetLength(1); x++)
        {
            if (arr[0, x] != 0)
            {
                leaderKey = arr[0, x];
                break;
            }
        }

        Vector3 camTarget = mainCamera.transform.position;

        if (leaderKey != 0)
        {
            GameObject leaderObj = CharacterManager.Instance.GetCharacterObject(leaderKey);
            if (leaderObj != null)
            {
                Vector3 leaderPos = leaderObj.transform.position;

                Vector3 offset = new Vector3(2f, 1f, 0); // 원하는 오프셋
                camTarget = leaderPos + offset;

                camTarget.z = mainCamera.transform.position.z;
            }
        }
        else
        {
            camTarget = mainCamera.transform.position + new Vector3(delta.x, delta.y, 0);
        }

        if (mainCamera != null)
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camTarget, 7f * Time.deltaTime);

        for (int i = 0; i < tilemaps.Length; i++)
            tilemaps[i].position -= delta;

        for (int i = 0; i < tilemaps.Length; i++)
        {
            Transform cur = tilemaps[i];
            Transform other = tilemaps[(i + 1) % tilemaps.Length];
            float distFromCam = Vector3.Dot(cur.position - mainCamera.transform.position, -moveDir);

            if (distFromCam > loopOffset.magnitude * 0.95f)
            {
                cur.position = other.position + loopOffset;
                Debug.Log($"[Loop] Move tilemap {i} to {cur.position}");
            }
        }
    }


}
