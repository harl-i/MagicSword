#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTile : TileBase
{
    public Sprite customSprite;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = customSprite;
    }

#if UNITY_EDITOR
    // Добавление опции в меню для создания CustomTile
    [MenuItem("Assets/Create/LevelTile")]
    public static void CreateCustomTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Level Tile", "New Level Tile", "Asset", "Save Level Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<LevelTile>(), path);
    }
#endif
}
