using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Pathfinding
{
    // class to initialize and manage all the nodes and the grid
    public class NodeManager : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject BytePrefab;
        public GameObject ObstaclePrefab;
        public GameObject ParticlePrefab;

        [Header("grid width, height and spacing")]
        // the length of the grid on the x and y axis respectively
        public int Width;
        public int Height;
        // the spacing between each node both vertically and horizontally
        public float CellSize;

        [Header("Reference Variables")]
        public Transform ImageTarget;
        public GameObject Back;
        public RansomMan.ByteTracker BTScript;
        public RansomMan.BackupSpawner BSScript;

        // grid object of nodes
        public PFGrid<Node> grid;

        void Start()
        {
            StartCoroutine(UpdatePosition());

            Back.transform.localScale = new Vector3((Width * CellSize), (Height * CellSize), 0.01f);
            Back.transform.position = new Vector3(ImageTarget.position.x, ImageTarget.position.y, ImageTarget.position.z + 0.05f);
            Back.SetActive(false);

            grid = new PFGrid<Node>(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid.Get(x, y).SetPosition(x, y);
                }
            }

            SetupRansomManMap();

            BSScript.nm = this;
            BSScript.CreateQuads();
        }

        IEnumerator UpdatePosition()
        {
            for (; ; )
            {
                SetPositionInfronOfImageTarget();
                yield return new WaitForSeconds(1f);
            }
        }

        public List<Node> GetAllByteNodes()
        {
            List<Node> byteNodes = new List<Node>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Node n = grid.Get(x, y);
                    if (!n.Obstacle && !n.Empty)
                    {
                        byteNodes.Add(n);
                    }
                }
            }

            return byteNodes;
        }

        void SetPositionInfronOfImageTarget()
        {
            transform.position = ImageTarget.position - ImageTarget.forward;

            transform.position = new Vector3(
                transform.position.x - (Mathf.Ceil(Width / 2) * CellSize),
                transform.position.y - (Mathf.Ceil(Height / 2) * CellSize) + 1,
                transform.position.z
            );
        }

        public void SpawnMapPrefabs()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Node n = grid.Get(x, y);
            
                    if (!n.Empty)
                    {
                        GameObject cube = null;
                        if (n.Obstacle)
                        {
                            cube = Instantiate(ObstaclePrefab, GetNodeWorldPosition(n), Quaternion.identity);

                            n.Object = cube;
                        }
                        else
                        {
                            cube = Instantiate(BytePrefab, GetNodeWorldPosition(n), Quaternion.identity);

                            n.Object = cube;

                            n.Particle = Instantiate(ParticlePrefab, GetNodeWorldPosition(n), Quaternion.Euler(180f, 0f, 0f));

                            n.Particle.SetActive(false);
                        }
                        cube.transform.parent = transform;
                    }
                    else
                    {
                        grid.Get(x, y).Object = null;
                    }
                }
            }
            Back.SetActive(true);

            BTScript.SetTotal();

            BSScript.SpawnBackup(0);
            BSScript.SpawnBackup(1);
            BSScript.SpawnBackup(2);
            BSScript.SpawnBackup(3);
        }

        void SetupRansomManMap()
        {
            char[,] map = GetMap();

            for (int a = 0; a < map.GetLength(1); a++)
            {
                for (int b = 0; b < map.GetLength(0); b++)
                {
                    grid.Get(b, a).Obstacle = map[b, a] == '1' ? true : false;
                    grid.Get(b, a).Empty = map[b, a] == '2' ? true : false;
                }
            }
        }

        char[,] GetMap()
        {
            string content = "";

            content = Resources.Load<TextAsset>("RansomManMap").text;

            char[,] map = new char[Width, Height];

            for (int a = 0; a < map.GetLength(1); a++)
            {
                for (int b = 0; b < map.GetLength(0); b++)
                {
                    map[b, (map.GetLength(1) - 1 - a)] = content.Split("\n")[a][b];
                }
            }

            return map;
        }

        // function for getting the node that is closest to the given world position
        public Node GetNearestNodeToPosition(Vector3 pos)
        {
            float closestDist = Mathf.Infinity;
            Node closest = null;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Node node = grid.Get(x, y);
                    Vector3 nodePos = GetNodeWorldPosition(node);
                    float dist = Vector3.Distance(pos, nodePos);

                    if (dist < closestDist && !node.Obstacle)
                    {
                        closestDist = dist;
                        closest = node;
                    }
                }
            }

            return closest;
        }

        // overload function to that takes in a minimum distance
        public Node GetNearestNodeToPosition(Vector3 pos, float minDist)
        {
            float closestDist = Mathf.Infinity;
            Node closest = null;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Node node = grid.Get(x, y);
                    Vector3 nodePos = GetNodeWorldPosition(node);
                    float dist = Vector3.Distance(pos, nodePos);

                    if (dist < closestDist && !node.Obstacle)
                    {
                        closestDist = dist;
                        closest = node;
                    }
                }
            }

            return Vector3.Distance(pos, GetNodeWorldPosition(closest)) <= minDist ? closest : null;
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
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid.Get(x, y).ResetValues();
                }
            }
        }
    }
}
