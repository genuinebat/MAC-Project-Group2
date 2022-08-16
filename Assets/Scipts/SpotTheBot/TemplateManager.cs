using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OKB
{
    public class TemplateManager : MonoBehaviour
    {
        [System.Serializable]
        public struct BotwareTemplate
        {
            public string Name;
            public GameObject Template;
            public bool FirstStatementCorrect, SecondStatementCorrect, ThirdStatementCorrect;

            public BotwareTemplate(string _n, GameObject _temp, bool _f, bool _s, bool _t)
            {
                Name = _n;
                Template = _temp;
                FirstStatementCorrect = _f;
                SecondStatementCorrect = _s;
                ThirdStatementCorrect = _t;
            }
        }

        public List<BotwareTemplate> BotwareCards;

        Swipe swipeScript;

        int current;

        void Start()
        {
            swipeScript = GetComponent<Swipe>();

            SetNewBot();
        }

        int NewCurrent()
        {
            return Random.Range(0, BotwareCards.Count - 1);
        }

        public void SetNewBot()
        {
            current = NewCurrent();

            swipeScript.Active = true;
        }

        public void GoodBot()
        {

        }

        public void BadBot()
        {

        }
    }
}