using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OKB
{
    public class BotContents : MonoBehaviour
    {
        public BotDetails Botwares;

        [System.Serializable]
        public class Bot
        {
            public string name;
            public string[] statements;
            public bool[] correct;
            public string[] reasons;
        }

        [System.Serializable]
        public class BotDetails
        {
            public Bot[] botwares;
        }

        void Start()
        {
            TextAsset contents = Resources.Load<TextAsset>("Puzzle4Contents");

            Botwares = JsonUtility.FromJson<BotDetails>(contents.text);
        }
    }
}
