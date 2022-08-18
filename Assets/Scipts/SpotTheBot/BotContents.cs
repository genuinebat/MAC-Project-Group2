using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OKB
{
    public class BotContents : MonoBehaviour
    {
        [HideInInspector]
        public Puzzle4Contents Contents;


        [System.Serializable]
        public class Bot
        {
            public string name;
            public string[] statements;
            public bool[] correct;
            public string[] reasons;
        }

        [System.Serializable]
        public class Puzzle4Contents
        {
            public Bot[] botwares;
        }

        void Start()
        {
            TextAsset jsonContents = Resources.Load<TextAsset>("Puzzle4Contents");
            Contents = JsonUtility.FromJson<Puzzle4Contents>(jsonContents.text);
        }
    }
}