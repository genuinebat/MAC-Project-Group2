// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Pathfinding;

// public class Test : MonoBehaviour
// {
//     [System.Serializable]
//     public struct JustTesting
//     {
//         // variables
//         public string Name;
//         public GameObject Template;
//         public bool First, Second, Third;

//         // constructor
//         public JustTesting(string _name, GameObject _temp, bool _f, bool _s, bool _t)
//         {
//             Name = _name;
//             Template = _temp;
//             First = _f;
//             Second = _s;
//             Third = _t;
//         }
//     }

//     public List<JustTesting> Botwares;

//     void Update()
//     {
//         int decide = Random.Range(
//             0,
//             Botwares.Count - 1
//         );

//         Botwares.RemoveAt(decide);
//     }
// }