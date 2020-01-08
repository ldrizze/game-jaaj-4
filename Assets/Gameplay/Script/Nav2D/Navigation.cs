using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nav2D
{
    enum NavAlgorithm
    {
        A_Star,
        Dijkstra
    }

    [RequireComponent(typeof(Tilemap))]
    public class Navigation : MonoBehaviour
    {
        [SerializeField]
        NavAlgorithm navigationAlgorithm = NavAlgorithm.A_Star;

        Tilemap tilemap = null;
        int offsetX = 0;
        int offsetY = 0;
        int width = 0;
        int height = 0;
        BaseNode[,] graph;


        void Start()
        {
            SetupNavigation();
        }

        void Update()
        {
        }

        void SetupNavigation()
        {
            tilemap = GetComponent<Tilemap>();

            if (!tilemap)
                return;

            offsetX = tilemap.cellBounds.x;
            offsetY = tilemap.cellBounds.y;

            width = tilemap.size.x;
            height = tilemap.size.y;

            graph = new BaseNode[height, width];

            for(int _y = 0;_y < height; _y++)
            {
                for (int _x = 0; _x < width; _x++)
                {
                    int x = offsetX + _x, y = offsetY + _y;

                    if(navigationAlgorithm == NavAlgorithm.A_Star)
                    {
                        graph[y, x] = new ANode();
                        graph[y, x].x = x;
                        graph[y, x].y = y;
                    }
                    else
                    {
                        graph[y, x] = new DNode();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!tilemap)
                SetupNavigation();

            DrawTileConnections();
        }

        void DrawTileConnections()
        {
            if (!tilemap)
                return;

            Gizmos.color = new Color(1f, 1f, 0f, .12f);

            for (int _y = 0; _y < height; _y++)
            {
                for (int _x = 0; _x < width; _x++)
                {
                    int x = offsetX + _x, // cell x coordinate
                        y = offsetY + _y, // cell y coordinate
                        z = 0; // cell z coordinate

                    Vector3Int vt = new Vector3Int(x, y, z),
                               vrt = new Vector3Int(x + 1, y, z),
                               vut = new Vector3Int(x, y + 1, z),
                               vrut = new Vector3Int(x + 1, y + 1, z);

                    Vector3 t = tilemap.GetCellCenterWorld(vt);
                    Vector3 rt = tilemap.GetCellCenterWorld(vrt);
                    Vector3 ut = tilemap.GetCellCenterWorld(vut);
                    Vector3 rut = tilemap.GetCellCenterWorld(vrut);

                    if(_x < width - 1)
                        Gizmos.DrawLine(t, rt);

                    if(_y < height - 1)
                        Gizmos.DrawLine(t, ut);

                    if (_x < width - 1 && _y < height - 1)
                        Gizmos.DrawLine(t, rut);
                }
            }
        }
    }

}