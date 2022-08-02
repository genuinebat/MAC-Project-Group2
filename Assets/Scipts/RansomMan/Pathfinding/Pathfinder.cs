using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder
    {
        // the move cost for the respective directions
        const int StraightMoveCost = 10;
        const int DiagonalMoveCost = 14;

        bool diagonal;

        NodeManager nm;

        List<Node> open, closed;

        public Pathfinder(NodeManager _nm, bool _diagonal)
        {
            nm = _nm;
            diagonal = _diagonal;
        }

        // function that is used to get a list of vector3 that form the shortest path from a start position to an end position
        public List<Vector3> GetPath(Vector3 start, Vector3 end)
        {
            List<Vector3> path = new List<Vector3>();

            foreach (Node node in FindPath(
                nm.GetNearestNodeToPosition(start),
                nm.GetNearestNodeToPosition(end)
            ))
            {
                path.Add(nm.GetNodeWorldPosition(node));
            }

            return path;
        }

        // functon for getting the list of nodes that form the shortest path from a starting node to an ending node
        List<Node> FindPath(Node start, Node end)
        {
            // an open list to store the nodes that are to be searched in the next iteration of the main loop
            open = new List<Node>() { start };
            // a closed list to store the nodes that no longer need to be searched
            closed = new List<Node>();

            nm.ResetNodes();

            start.G = 0;
            start.H = CalculateMoveCost(start, end);

            // main loop that only ends if the path has been found of the path is not possible
            while (open.Count > 0)
            {
                Node current = GetLowestFCost();

                if (current == end) return CalculatePath(end);

                open.Remove(current);
                closed.Add(current);

                foreach (Node node in nm.GetNeighbourNodes(current, diagonal))
                {
                    // continue if the neighbornode does not need to be searched
                    if (closed.Contains(node)) continue;

                    // continue as well if the node is an obstacle, adding the obstacle to the closed list
                    if (node.Obstacle)
                    {
                        closed.Add(node);
                        continue;
                    }

                    // the G cost of the neighbor node can be calculated by adding the current node's G cost and the movement cost based on the direction of the neighbor node (either 10 or 14)
                    int gCost = current.G + CalculateMoveCost(current, node);
                    
                    // since the nodes are initialized with as infinite amount, and nodes that have already been searched would never have a lower G cost, this ensures that the search will always be for a node that has not already been searched
                    if (gCost < node.G)
                    {
                        node.PreviousNode = current;
                        node.G = gCost;
                        node.H = CalculateMoveCost(node, end);

                        if (!open.Contains(node)) open.Add(node);
                    }
                }
            }

            return new List<Node>();
        }

        // function that multiplies the diagonal move cost by the overlapping values of the x and y, then adding the product of the straight move cost by the remainder (can be either x or y)
        // this results in the move cost of the shortest path from node a to node b
        int CalculateMoveCost(Node a, Node b)
        {
            int x = Mathf.Abs(a.GridX - b.GridX);
            int y = Mathf.Abs(a.GridY - b.GridY);
            int remainder = Mathf.Abs(x - y);

            return (DiagonalMoveCost * Mathf.Min(x, y)) + (StraightMoveCost * remainder);
        }

        // function that returns the node with the lowest F cost in the open list
        Node GetLowestFCost()
        {
            int lowestF = int.MaxValue;
            Node n = null;

            foreach(Node node in open)
            {
                if (node.F < lowestF)
                {
                    lowestF = node.F;
                    n = node;
                }
            }
            
            return n;
        }

        // function that starts from the end node and trace back the path that the algorithm took to get to the end node which results in the shortest path from the end node to the start node
        // after that the list is simply reversed to get the correct direction
        List<Node> CalculatePath(Node end)
        {
            List<Node> path = new List<Node>();
            Node node = end;

            while (node.PreviousNode != null)
            {
                path.Add(node);
                node = node.PreviousNode;
            }

            path.Reverse();
            return path;
        }
    }
}

