using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{
    public Tilemap mainTilemap; // �������� Tilemap �����
    public GameObject[] levelBlockPrefabs; // ������ �������� ������ ������
    public Transform playerTransform; // ��������� ������

    private Vector3Int nextStartPosition = new Vector3Int(0, 0, 0); // ��������� ������� ��� ���������� �����
    private float lastBlockHeight = 0; // ������ ���������� ������������ �����

    private void Start()
    {
        AddLevelBlock(0);
    }

    private void Update()
    {
        // ������� ������ �� ��� Y ������������ Tilemap
        float playerPositionY = playerTransform.position.y - mainTilemap.transform.position.y;

        // ���� ����� ����������� � ����� �������� �����, ��������� ���������
        if (playerPositionY > nextStartPosition.y - lastBlockHeight * 1f)
        {
            AddLevelBlock(Random.Range(0, levelBlockPrefabs.Length));
        }
    }

    // ������� ��� ���������� ����� �� �������� Tilemap
    public void AddLevelBlock(int blockIndex)
    {
        GameObject blockPrefab = Instantiate(levelBlockPrefabs[blockIndex]);
        Tilemap blockTilemap = blockPrefab.GetComponentInChildren<Tilemap>();

        // ����������� ������ �� ������� ����� � �������� Tilemap
        foreach (var pos in blockTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int tilePosition = new Vector3Int(pos.x, pos.y, pos.z);
            TileBase tile = blockTilemap.GetTile(tilePosition);
            if (tile != null)
            {
                mainTilemap.SetTile(tilePosition + nextStartPosition, tile);
            }
        }

        // ���������� ��������� ��������� ������� � ������ �����
        lastBlockHeight = blockTilemap.size.y * mainTilemap.cellSize.y;
        nextStartPosition += new Vector3Int(0, blockTilemap.size.y, 0);

        // �������� ���������� ������� ����� �� �����
        Destroy(blockPrefab);
    }
}
