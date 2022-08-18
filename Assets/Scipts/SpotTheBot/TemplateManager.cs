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

        GameObject selectStatement1Back, selectStatement2Back, selectStatement3Back;

        TMP_Text statement1, statement2, statement3, selectStatement1, selectStatement2, selectStatement3;

        BotContents bc;
        Swipe sw;

        List<int> botwareCards = new List<int>();

        int current;

        void Start()
        {
            bc = GetComponent<BotContents>();
            sw = GetComponent<Swipe>();
            
            statement1 = Template.transform.Find("SwipeCanvas").Find("Back").Find("Statement1").gameObject.GetComponent<TMP_Text>();
            statement2 = Template.transform.Find("SwipeCanvas").Find("Back").Find("Statement2").gameObject.GetComponent<TMP_Text>();
            statement3 = Template.transform.Find("SwipeCanvas").Find("Back").Find("Statement3").gameObject.GetComponent<TMP_Text>();

            selectStatement1 = StatementSelect.transform.Find("Background").Find("Statement1").gameObject.GetComponent<TMP_Text>();
            selectStatement2 = StatementSelect.transform.Find("Background").Find("Statement2").gameObject.GetComponent<TMP_Text>();
            selectStatement3 = StatementSelect.transform.Find("Background").Find("Statement3").gameObject.GetComponent<TMP_Text>();

            selectStatement1Back = StatementSelect.transform.Find("Background").Find("Statement1Back").gameObject;
            selectStatement2Back = StatementSelect.transform.Find("Background").Find("Statement2Back").gameObject;
            selectStatement3Back = StatementSelect.transform.Find("Background").Find("Statement3Back").gameObject;

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

        public void SetNewBot()
        {
            current = NewCurrent();

            if (current < 0) return;

            // set statements over here
            statement1.text = "just testing";

            sw.Active = true;
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

        public void SelectStatement(int s)
        {
            bool check = TempStatementCorrect.Contains(s);
            if (check)
            {
                TempStatementCorrect.Remove(s);
            }
            else
            {
                TempStatementCorrect.Add(s);
            }

            switch (s)
            {
                case 0:
                    if (check) selectStatement1Back.SetActive(false);
                    else selectStatement1Back.SetActive(true);
                    break;
                
                case 1:
                    if (check) selectStatement2Back.SetActive(false);
                    else selectStatement2Back.SetActive(true);
                    break;

                case 2:
                    if (check) selectStatement3Back.SetActive(false);
                    else selectStatement3Back.SetActive(true);
                    break;
            }
        }

        public void SetStatementSelect()
        {
            // set the statements

            TempStatementCorrect.Clear();
            StatementSelect.SetActive(true);
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