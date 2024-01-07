using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{
    public Tilemap mainTilemap; // Основная Tilemap сцены
    public GameObject[] levelBlockPrefabs; // Массив префабов блоков уровня
    public Transform playerTransform; // Трансформ игрока

    private Vector3Int nextStartPosition = new Vector3Int(0, 0, 0); // Стартовая позиция для следующего блока
    private float lastBlockHeight = 0; // Высота последнего добавленного блока

    private void Start()
    {
        AddLevelBlock(0);
    }

    private void Update()
    {
        // Позиция игрока по оси Y относительно Tilemap
        float playerPositionY = playerTransform.position.y - mainTilemap.transform.position.y;

        // Если игрок приблизился к концу текущего блока, добавляем следующий
        if (playerPositionY > nextStartPosition.y - lastBlockHeight * 1f)
        {
            AddLevelBlock(Random.Range(0, levelBlockPrefabs.Length));
        }
    }

    // Функция для добавления блока на основную Tilemap
    public void AddLevelBlock(int blockIndex)
    {
        GameObject blockPrefab = Instantiate(levelBlockPrefabs[blockIndex]);
        Tilemap blockTilemap = blockPrefab.GetComponentInChildren<Tilemap>();

        // Копирование тайлов из префаба блока в основную Tilemap
        foreach (var pos in blockTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int tilePosition = new Vector3Int(pos.x, pos.y, pos.z);
            TileBase tile = blockTilemap.GetTile(tilePosition);
            if (tile != null)
            {
                mainTilemap.SetTile(tilePosition + nextStartPosition, tile);
            }
        }

        // Обновление последней стартовой позиции и высоты блока
        lastBlockHeight = blockTilemap.size.y * mainTilemap.cellSize.y;
        nextStartPosition += new Vector3Int(0, blockTilemap.size.y, 0);

        // Удаление временного префаба блока из сцены
        Destroy(blockPrefab);
    }
}
