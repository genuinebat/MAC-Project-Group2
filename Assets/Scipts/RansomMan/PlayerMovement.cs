using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace RansomMan
{
    public class PlayerMovement : MonoBehaviour
    {
        NodeManager nm;
        Pathfinder pf;

        void Start()
        {
            nm = GameObject.Find("NodeManager").GetComponent<NodeManager>();
            pf = new Pathfinder(nm, false);
        }

        void Update()
        {
            
        }

        public void PlayerStartPosition()
        {
            transform.position = nm.GetNodeWorldPosition(nm.grid.Get(13, 7));
        }
    }
}