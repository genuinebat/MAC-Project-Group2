using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    // class to initialize and manage all the nodes and the grid
    public class NodeManager : MonoBehaviour
    {
        // the length of the grid on the x and y axis respectively
        public int Width = 40;
        public int Height = 40;
        // the spacing between each node both vertically and horizontally
        public float CellSize = 1f;

        // grid object of nodes
        PFGrid<Node> grid;

        void Start()
        {
            grid = new PFGrid<Node>(Width, Height);

            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    grid.Get(x, y).SetPosition(x, y);
                }
            }

            // checking and setting whichever nodes overlap with obstacles
            foreach (GameObject obst in GameObject.FindGameObjectsWithTag("Obstacle"))
            {
                Renderer r = obst.GetComponent<Renderer>();
                SetObstacle(r.bounds.center, r.bounds.size.x, r.bounds.size.z);
            }
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
        public List<Node> GetNeighbourNodes(Node curr)
        {
            List<Node> list = new List<Node>();

            if (curr.GridX > 0)
            {
                // left node
                list.Add(grid.Get(curr.GridX - 1, curr.GridY));

                // bottom left node
                if (curr.GridY > 0) list.Add(grid.Get(curr.GridX - 1, curr.GridY - 1));

                // top left node
                if (curr.GridY < Height - 1) list.Add(grid.Get(curr.GridX - 1, curr.GridY + 1));
            }

            if (curr.GridY > 0)
            {
                // bottom node
                list.Add(grid.Get(curr.GridX, curr.GridY - 1));

                // bottom right node
                if (curr.GridX > 0) list.Add(grid.Get(curr.GridX + 1, curr.GridY - 1));

                // top right node
                if (curr.GridX < Width - 1) list.Add(grid.Get(curr.GridX + 1, curr.GridY + 1));
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
                transform.position.y,
                transform.position.z + (node.GridY * CellSize)
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

        // function that sets the respective nodes that overlap with the given object
        void SetObstacle(Vector3 obstPos, float obstWidth, float obstHeight)
        {
            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    Node n = grid.Get(x, y);
                    Vector3 pos = GetNodeWorldPosition(n);

                    if (
                        pos.x <= (obstPos.x + Mathf.Ceil(obstWidth / 2)) &&
                        pos.x >= (obstPos.x - Mathf.Ceil(obstWidth / 2)) &&
                        pos.z <= (obstPos.z + Mathf.Ceil(obstHeight / 2)) &&
                        pos.z >= (obstPos.z - Mathf.Ceil(obstHeight / 2))
                    ) n.Obstacle = true;
                }
            }
        }
    }
}
