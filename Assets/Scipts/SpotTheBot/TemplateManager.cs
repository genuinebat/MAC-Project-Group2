using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OKB
{
    public class TemplateManager : MonoBehaviour
    {   
        public GameObject Template;

        Swipe sw;

        List<int> botwareCards;

        int current;

        void Start()
        {
            sw = GetComponent<Swipe>();

            botwareCards.Add(0);
            botwareCards.Add(1);
            botwareCards.Add(2);
            botwareCards.Add(3);
            botwareCards.Add(4);
            botwareCards.Add(5);
            botwareCards.Add(6);
            botwareCards.Add(7);
            botwareCards.Add(8);
            botwareCards.Add(9);
        }

        int NewCurrent()
        {
            int a = Random.Range(0, botwareCards.Count - 1);

            int c = botwareCards[a];

            botwareCards.RemoveAt(a);

            return c;
        }

        public void SetNewBot()
        {
            current = NewCurrent();

            sw.Active = true;
        }

        public void GoodBot()
        {

        }

        public void BadBot()
        {

        }
    }
}