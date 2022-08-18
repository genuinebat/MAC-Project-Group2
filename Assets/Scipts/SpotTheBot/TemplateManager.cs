using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OKB
{
    public class TemplateManager : MonoBehaviour
    {
        [Header("Reference Variables")]
        public GameObject Template;
        public GameObject CorrectPanel;
        public GameObject WrongPanel;
        public GameObject StatementSelect;

        [HideInInspector]
        public List<int> TempStatementCorrect;

        GameObject statements;

        BotContents bc;
        Swipe sw;

        List<int> botwareCards;

        int current;

        void Start()
        {
            bc = GetComponent<BotContents>();
            sw = GetComponent<Swipe>();

            StatementSelect.SetActive(false);

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
            if (botwareCards.Count == 0)
            {
                StartTrojan();
                return -1;
            }

            int a = Random.Range(0, botwareCards.Count - 1);

            int c = botwareCards[a];

            botwareCards.RemoveAt(a);

            return c;
        }

        public void SelectStatement(int s, GameObject back)
        {
            if (TempStatementCorrect.Contains(s))
            {
                TempStatementCorrect.Remove(s);
                back.SetActive(false);
            }
            else
            {
                TempStatementCorrect.Add(s);
                back.SetActive(true);
            }
        }

        public void SetStatementSelect()
        {
            // set the statements

            TempStatementCorrect.Clear();
            StatementSelect.SetActive(true);
        }

        public void SetNewBot()
        {
            current = NewCurrent();

            if (current < 0) return;


            sw.Active = true;
        }

        public void GoodBot()
        {
            // if current bot not all correct
            // lose health flash red
            // show reasons
            // else flash green
            // next bot
        }

        public void BadBot()
        {
            List<int> wrong = new List<int>();

            // if current bot correct does not match TempStatementCorrect
            // lose health flash red
            // show reasons based on which the player got wrong
            // else flash green
            // next bot
        }

        IEnumerator FlashGreen()
        {
            CorrectPanel.SetActive(true);
            WrongPanel.SetActive(false);

            yield return new WaitForSeconds(0.2f);

            CorrectPanel.SetActive(false);
            WrongPanel.SetActive(false);
        }

        IEnumerator FlashRed()
        {
            CorrectPanel.SetActive(false);
            WrongPanel.SetActive(true);

            yield return new WaitForSeconds(0.2f);

            CorrectPanel.SetActive(false);
            WrongPanel.SetActive(false);
        }

        void StartTrojan()
        {
            // trojan botware spawns
        }
    }
}