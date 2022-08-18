using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OKB
{
    public class BotContent : MonoBehaviour
    {
        public TextAsset BotDetailsJson;

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

        public BotDetails BotArray = new BotDetails();
        //public Bot BotDetails = new Bot();
        // Start is called before the first frame update
        void Start()
        {
            BotArray = JsonUtility.FromJson<BotDetails>(BotDetailsJson.text);
            //BotDetails = JsonUtility.FromJson<Bot>(BotDetails.text);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
