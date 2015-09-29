using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class FloorTileCreator : EditorWindow {

    GameObject TileObject;
    float width;
    float height;
    float spacing;

    List<List<GameObject>> tiles = new List<List<GameObject>>();

    [MenuItem("Window/FloorCreator")]
    public static void GetWindow()
    {
        EditorWindow.GetWindow(typeof(FloorTileCreator));
    }

    void OnGUI()
    {
        TileObject = (GameObject)EditorGUI.ObjectField(new Rect(4, 4, position.width - 8, 16), "Tile Object", TileObject, typeof(GameObject));
        width = EditorGUI.FloatField(new Rect(4, 24, position.width - 8, 16), "Width", width);
        height = EditorGUI.FloatField(new Rect(4, 44, position.width - 8, 16), "Height", height);
        spacing = EditorGUI.FloatField(new Rect(4, 64, position.width - 8, 16), "Spacing", spacing);

        if (TileObject)
        {
            if (GUI.Button(new Rect(4, 84, position.width - 8, 24), "Create Tiles"))
            {
                SpawnTiles();
            }

            if (GUI.Button(new Rect(4, 112, position.width - 8, 24), "Remove Tiles"))
            {
                DestroyTiles();
            }
        }
    }

    void SpawnTiles()
    {
        if (tiles.Count > 0)
            DestroyTiles();

        Transform tileContainer = GameObject.FindGameObjectWithTag("TileContainer").transform;

        for (int i = 0; i < height; i++) //rows
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < width; j++) //cols
            {
                Vector3 atPosition = new Vector3(j * spacing, 0, i * spacing);
                GameObject go = Instantiate(TileObject) as GameObject;
                go.transform.position = atPosition;
                go.transform.parent = tileContainer;
                row.Add(go);
            }
            tiles.Add(row);
        }
    }

    void DestroyTiles()
    {
        if (tiles.Count > 0)
        {
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                List<GameObject> row = tiles[i];
                for (int j = row.Count - 1; j >= 0; j--)
                {
                    GameObject.DestroyImmediate(row[j]);
                }
                tiles.RemoveAt(i);
            }
        }
    }
}
