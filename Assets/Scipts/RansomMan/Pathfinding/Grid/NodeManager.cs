using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Pathfinding
{
    // class to initialize and manage all the nodes and the grid
    public class NodeManager : MonoBehaviour
    {
        public GameObject temp1, temp2;
        // the length of the grid on the x and y axis respectively
        [Header("grid width, height and spacing")]
        public int Width;
        public int Height;
        // the spacing between each node both vertically and horizontally
        public float CellSize;

        // grid object of nodes
        PFGrid<Node> grid;

        void Start()
        {
            Width++;
            Height++;

            transform.position = new Vector3(
                transform.position.x - (Mathf.Ceil(Width / 2) * CellSize),
                transform.position.y - (Mathf.Ceil(Height / 2) * CellSize),
                transform.position.z
            );

            grid = new PFGrid<Node>(Width, Height);

            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    grid.Get(x, y).SetPosition(x, y);
                }
            }

            SetupRansomManMap();

            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    if (grid.Get(x, y).Obstacle)
                    {
                        Instantiate(temp2, GetNodeWorldPosition(grid.Get(x, y)), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(temp1, GetNodeWorldPosition(grid.Get(x, y)), Quaternion.identity);
                    }
                }
            }
        }

        void SetupRansomManMap()
        {
            char[,] map = GetMap();

            Debug.Log(map[26, 28]);
            
            for (int a = 0; a < map.GetLength(1); a++)
            {
                for (int b = 0; b < map.GetLength(0); b++)
                {
                    grid.Get(b, a).Obstacle = map[b, a] == '1' ? true : false;
                }
            }
        }

        char[,] GetMap()
        {
            string content = File.ReadAllText(Application.dataPath + "/RansomManMap.txt");
            char[,] map =  new char[27, 29];

            for (int a = 0; a < map.GetLength(1); a++)
            {
                for (int b = 0; b < map.GetLength(0); b++)
                {
                    map[b, (map.GetLength(1) - 1- a)] = content.Split("\n")[a][b];
                }
            }

            return map;
        }

        // function for getting the node that is closest to the given world position
        public Node GetNearestNodeToPosition(Vector3 pos)
        {
            float closestDist = Mathf.Infinity;
            Node closest = null;

            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    Node node = grid.Get(x, y);
                    Vector3 nodePos = GetNodeWorldPosition(node);
                    float dist = Vector3.Distance(pos, nodePos);

                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closest = node;
                    }
                }
            }

            return closest;
        }

        // function for returning all neighboring nodes to the given node (minimum of 3, maximum of 8)
        public List<Node> GetNeighbourNodes(Node curr, bool diagonal)
        {
            List<Node> list = new List<Node>();

            if (curr.GridX > 0)
            {
                // left node
                list.Add(grid.Get(curr.GridX - 1, curr.GridY));

                // bottom left node
                if (curr.GridY > 0 && diagonal) list.Add(grid.Get(curr.GridX - 1, curr.GridY - 1));

                // top left node
                if (curr.GridY < Height - 1 && diagonal) list.Add(grid.Get(curr.GridX - 1, curr.GridY + 1));
            }

            if (curr.GridY > 0)
            {
                // bottom node
                list.Add(grid.Get(curr.GridX, curr.GridY - 1));

                // bottom right node
                if (curr.GridX > 0 && diagonal) list.Add(grid.Get(curr.GridX + 1, curr.GridY - 1));

                // top right node
                if (curr.GridX < Width - 1 && diagonal) list.Add(grid.Get(curr.GridX + 1, curr.GridY + 1));
            }

            // right node
            if (curr.GridX < Width - 1) list.Add(grid.Get(curr.GridX + 1, curr.GridY));
            
            // top node
            if (curr.GridY < Height - 1) list.Add(grid.Get(curr.GridX, curr.GridY + 1));
            
            return list;
        }

        // funciton that returns the given node's world position by calculating based on each cellsize, the x and y coordinates of the node, and the position of the origin point (which is the position of the game object the node manager is attached to)
        public Vector3 GetNodeWorldPosition(Node node)
        {
            return new Vector3(
                transform.position.x + (node.GridX * CellSize),
                transform.position.y + (node.GridY * CellSize),
                transform.position.z
            );
        }

        // function that resets all nodes for recalculation of path
        public void ResetNodes()
        {
            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    grid.Get(x, y).ResetValues();
                }
            }
        }
    }
}
