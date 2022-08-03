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
            nm = GameObject.Find("NodeManager");
            pf = new Pathfinder(nm, false);
        }

        void Update()
        {

        }
    }
}