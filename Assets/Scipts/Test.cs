using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;

public class Test : MonoBehaviour
{
    [System.Serializable]
    public struct JustTesting
    {
        // variables
        public string Name;
        public GameObject Template;
        public bool First, Second, Third;

        // constructor
        public JustTesting(string _name, GameObject _temp, bool _f, bool _s, bool _t)
        {
            Name = _name;
            Template = _temp;
            First = _f;
            Second = _s;
            Third = _t;
        }
    }
    public class DeletePlayerPrefsScript : EditorWindow
    {
        [MenuItem("Window/Delete PlayerPrefs (All)")]
        static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public List<JustTesting> Botwares;
}



