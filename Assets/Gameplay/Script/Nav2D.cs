using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Nav2D : MonoBehaviour
{
    Tilemap tilemap = null;
    int offsetX = 0;
    int offsetY = 0;
    int width = 0;
    int height = 0;
    float[,] graph;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        SetupNav();
    }

    void Update()
    {
    }

    void SetupNav()
    {
        if (!tilemap)
            return;

        offsetX = tilemap.cellBounds.x;
        offsetY = tilemap.cellBounds.y;

        width = tilemap.size.x;
        height = tilemap.size.y;

        graph = new float[height, width];

        for (int y=0; y<height; y++)
        {
            for(int x=0; x<width; x++)
            {
                Tile t = (Tile) tilemap.GetTile(new Vector3Int(offsetX + x, offsetY + y, 0));

                if (t)
                    graph[y, x] = float.MaxValue;
                else
                    graph[y, x] = 1f;
            }
        }

        Debug.Log(graph);
    }
}
