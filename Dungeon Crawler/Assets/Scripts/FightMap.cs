using UnityEngine;

public class FightMap: MonoBehaviour {
    public GameObject tilePrefab; // Assigner un prefab de case
    public GameObject playerPrefab; // Assigner un prefab de joueur
    private GameObject player;
    private int rows = 3, cols = 2;
    private Vector2 playerPos = new Vector2(0, 0);

    void Start() {
        CreateGrid();
        SpawnPlayer();
    }

    void CreateGrid() {
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                Vector3 pos = new Vector2(x, -y);
                Instantiate(tilePrefab, pos, Quaternion.identity);
            }
        }
    }

    void SpawnPlayer() {
        Vector3 startPosition = new Vector2(playerPos.x, -playerPos.y);
        player = Instantiate(playerPrefab, startPosition, Quaternion.identity);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) MovePlayer(Vector2.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) MovePlayer(Vector2.down);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MovePlayer(Vector2.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) MovePlayer(Vector2.right);
    }

    void MovePlayer(Vector2 direction) {
        Vector2 newPos = playerPos + direction;
        if (IsValidPosition(newPos)) {
            playerPos = newPos;
            player.transform.position = new Vector3(playerPos.x, -playerPos.y, 0);
        }
    }

    bool IsValidPosition(Vector2 pos) {
        return pos.x >= 0 && pos.x < cols && pos.y >= 0 && pos.y < rows;
    }
}

