using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    // this generic class is used for creating a figurative grid of a specified object
    public class PFGrid<T> where T : new()
    {
        // multi-dimensional array to store the grid
        T[,] gridArr;

        public PFGrid(int width, int height)
        {
            gridArr = new T[width, height];

            for (int x = 0; x < gridArr.GetLength(0); x++)
            {
                for (int y = 0; y < gridArr.GetLength(1); y++)
                {
                    gridArr[x, y] = new T();
                }
            }
        }

        // function to get the object at the specified coordinate
        public T Get(int x, int y)
        {
            return gridArr[x, y];
        }
    }
}
