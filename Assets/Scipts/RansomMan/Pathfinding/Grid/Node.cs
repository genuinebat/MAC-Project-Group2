using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    // this object will be used to store all of the data needed for the A* pathfinding algorithm
    public class Node
    {
        // the node that this node came from when calcualting the path
        public Node PreviousNode { get; set; }

        // bool for if this node overlaps with an obstacle obeject
        public bool Obstacle { get; set; }

        public bool Empty { get; set; }

        // the x and y coordinates of this node in the figurative grid created by the node manager
        public int GridX { get; private set; }
        public int GridY { get; private set; }

        // the cost to get to from the start node to this node taking into account for obstacles
        public int G { get; set; }
        // the estimated cost to get from this node to the end node ignoring all obstacles
        public int H { get; set; }
        // the sum of G cost and H cost
        public int F
        {
            get { return G + H; }
            set { }
        }

        public Node()
        {
            Obstacle = false;
        }

        public void SetPosition(int x, int y)
        {
            GridX = x;
            GridY = y;
        }

        // resets the node for when a new path is to be calculated
        public void ResetValues()
        {
            PreviousNode = null;

            G = int.MaxValue;
            H = 0;
        }
    }
}