using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace OKB
{
    public class TemplateManager : MonoBehaviour
    {
        [Header("Reference Variables")]
        public GameObject Template;
        public GameObject CorrectPanel;
        public GameObject WrongPanel;
        public GameObject ReasonPanel;
        public GameObject StatementSelect;
        public GameObject Bot;
        public GameObject Troy;
        public GameObject BonusQnPopup;
        public TMP_Text BotCounter;
        public OKBSM OKBSMScript;
        public int PlayerHealth;
        public int NumberOfHearts;
        public Sprite FullHeart;
        public Sprite EmptyHeart;
        public Image[] Hearts;

        [Header("Botware Parts")]
        public GameObject Face;
        public GameObject Halo;
        public GameObject Horns;
        public GameObject LWing;
        public GameObject RWing;
        public GameObject Nails;
        public Material[] Faces;

        int tryCount;

        [HideInInspector]
        public List<Image> HealthImages;

        [HideInInspector]
        public List<int> TempStatementWrong;

        GameObject selectStatement1Back, selectStatement2Back, selectStatement3Back;

        TMP_Text statement1, statement2, statement3, selectStatement1, selectStatement2, selectStatement3, reasonStatement1, reasonStatement2, reasonStatement3, botName;

        Button confBtn;

        BotContents bc;
        Swipe sw;

        List<int> botwareCards = new List<int>();

        int current;
        int totalBots;

        void Start()
        {
            bc = GetComponent<BotContents>();
            sw = GetComponent<Swipe>();
            PlayerHealth = Hearts.Length;

            //TMPro for the botname
            botName = Template.transform.Find("SwipeCanvas").Find("Back").Find("NameBack").Find("Name").gameObject.GetComponent<TMP_Text>();

            //TMPro for the deescription text
            statement1 = Template.transform.Find("SwipeCanvas").Find("Back").Find("Statement1").gameObject.GetComponent<TMP_Text>();
            statement2 = Template.transform.Find("SwipeCanvas").Find("Back").Find("Statement2").gameObject.GetComponent<TMP_Text>();
            statement3 = Template.transform.Find("SwipeCanvas").Find("Back").Find("Statement3").gameObject.GetComponent<TMP_Text>();

            //TMPro for the selection screen
            selectStatement1 = StatementSelect.transform.Find("Background").Find("Statement1").gameObject.GetComponent<TMP_Text>();
            selectStatement2 = StatementSelect.transform.Find("Background").Find("Statement2").gameObject.GetComponent<TMP_Text>();
            selectStatement3 = StatementSelect.transform.Find("Background").Find("Statement3").gameObject.GetComponent<TMP_Text>();

            //TMPro for the reasons
            reasonStatement1 = ReasonPanel.transform.Find("Background").Find("Reason Container").Find("Reason1").gameObject.GetComponent<TMP_Text>();
            reasonStatement2 = ReasonPanel.transform.Find("Background").Find("Reason Container").Find("Reason2").gameObject.GetComponent<TMP_Text>();
            reasonStatement3 = ReasonPanel.transform.Find("Background").Find("Reason Container").Find("Reason3").gameObject.GetComponent<TMP_Text>();

            selectStatement1Back = StatementSelect.transform.Find("Background").Find("Statement1Back").gameObject;
            selectStatement2Back = StatementSelect.transform.Find("Background").Find("Statement2Back").gameObject;
            selectStatement3Back = StatementSelect.transform.Find("Background").Find("Statement3Back").gameObject;

            confBtn = StatementSelect.transform.Find("Background").Find("ConfirmBtn").gameObject.GetComponent<Button>();

            StatementSelect.SetActive(false);
        }
        void Update()
        {
            for (int i = 0; i < Hearts.Length; i++)
            {
                if (i < PlayerHealth)
                {
                    Hearts[i].sprite = FullHeart;
                }
                else
                {
                    Hearts[i].sprite = EmptyHeart;
                }
            }
        }

        public void NewGameFunc()
        {
            PlayerHealth = 2;
            botwareCards.Clear();
            for (int i = 0; i < bc.Contents.botwares.Length; i++)
            {
                botwareCards.Add(i);
            }

            totalBots = botwareCards.Count;
        }

        public void SetNewBot()
        {
            Time.timeScale = 1;
            StatementSelect.SetActive(false);
            ReasonPanel.SetActive(false);

            current = NewCurrent();

            RandomiseAppearance();

            if (current == -1)
            {
                StartTrojan();
                return;
            }

            // set bot name
            botName.text = bc.Contents.botwares[current].name + " Bot";

            // set statements in the swiping cards
            statement1.text = bc.Contents.botwares[current].statements[0];
            statement2.text = bc.Contents.botwares[current].statements[1];
            statement3.text = bc.Contents.botwares[current].statements[2];

            sw.StopFly = true;
            StartCoroutine(SlideUp());
        }

        IEnumerator SlideUp()
        {
            Transform obj = Template.transform;

            obj.position = new Vector3(sw.ogPos.x, sw.ogPos.y - 5, sw.ogPos.z);

            obj.rotation = Quaternion.Euler(0f, 0f, 0f);

            while (Vector3.Distance(obj.position, sw.ogPos) > 0.02f)
            {
                obj.position = Vector3.MoveTowards(obj.position, sw.ogPos, 8 * Time.deltaTime);

                yield return null;
            }

            sw.Active = true;
        }

        void RandomiseAppearance()
        {
            int face = Random.Range(0, Faces.Length);
            bool wings = Random.Range(0, 2) == 1 ? true : false;
            bool nails = Random.Range(0, 2) == 1 ? true : false;
            bool halo = Random.Range(0, 2) == 1 ? true : false;
            bool horns = Random.Range(0, 2) == 1 ? true : false;

            Face.GetComponent<Renderer>().material = Faces[face];

            if (wings)
            {
                RWing.SetActive(true);
                LWing.SetActive(true);
            }
            else
            {
                RWing.SetActive(false);
                LWing.SetActive(false);
            }

            if (nails) Nails.SetActive(true);
            else Nails.SetActive(false);

            if (halo) Halo.SetActive(true);
            else Halo.SetActive(false);

            if (horns) Horns.SetActive(true);
            else Horns.SetActive(false);
        }

        int NewCurrent()
        {
            if (botwareCards.Count == 0)
            {
                return -1;
            }

            int a = Random.Range(0, botwareCards.Count);

            int c = botwareCards[a];

            botwareCards.RemoveAt(a);

            BotCounter.text = "Botwares: " + (totalBots - botwareCards.Count).ToString() + " / " + totalBots;

            return c;
        }

        public void SelectStatement(int s)
        {
            bool check = TempStatementWrong.Contains(s);
            if (check)
            {
                TempStatementWrong.Remove(s);
            }
            else
            {
                TempStatementWrong.Add(s);
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

            confBtn.interactable = TempStatementWrong.Count > 0 ? true : false;
        }

        public void SetStatementSelect()
        {
            // sets statements in the picking page
            selectStatement1.text = (bc.Contents.botwares[current].statements[0]);
            selectStatement2.text = (bc.Contents.botwares[current].statements[1]);
            selectStatement3.text = (bc.Contents.botwares[current].statements[2]);

            TempStatementWrong.Clear();
            confBtn.interactable = false;

            selectStatement1Back.SetActive(false);
            selectStatement2Back.SetActive(false);
            selectStatement3Back.SetActive(false);

            StatementSelect.SetActive(true);
        }

        public void GoodBot()
        {
            // list containing the index of the statements the player got wrong
            List<int> wrong = new List<int>();

            bool[] corr = bc.Contents.botwares[current].correct;

            for (int i = 0; i < corr.Length; i++)
            {
                if (!corr[i]) wrong.Add(i);
            }

            if (wrong.Count > 0)
            {
                StartCoroutine(FlashRed(wrong));
            }
            else
            {
                StartCoroutine(FlashGreen());
            }
        }

        public void BadBot()
        {
            // list containing the index of the statements the player got wrong
            List<int> wrong = new List<int>();

            bool[] corr = bc.Contents.botwares[current].correct;

            for (int i = 0; i < corr.Length; i++)
            {
                if (!corr[i] && !TempStatementWrong.Contains(i))
                {
                    wrong.Add(i);

                }
                else if (corr[i] && TempStatementWrong.Contains(i))
                {
                    wrong.Add(i);
                }
            }

            if (wrong.Count > 0)
            {
                StartCoroutine(FlashRed(wrong));
            }
            else
            {
                StartCoroutine(FlashGreen());
            }
        }

        void ReasonPopup(List<int> wrongAns)
        {
            Time.timeScale = 0;
            ReasonPanel.SetActive(true);

            foreach (Transform child in ReasonPanel.transform.Find("Background").Find("Reason Container"))
            {
                child.gameObject.SetActive(false);
            }

            for (int i = 0; i < wrongAns.Count; i++)
            {
                GameObject currentReason = ReasonPanel.transform.Find("Background").Find("Reason Container").GetChild(i).gameObject;

                currentReason.SetActive(true);
                currentReason.GetComponent<TMP_Text>().text = "STATEMENT " + (wrongAns[i] + 1).ToString() + " REASON:\n" + bc.Contents.botwares[current].reasons[wrongAns[i]];
            }
        }

        public void CloseReasonPopup()
        {
            if (PlayerHealth <= 0)
            {
                sw.GoodBotPanel.SetActive(false);
                sw.BadBotPanel.SetActive(false);
                LoseGame();
            }
            else
            {
                SetNewBot();
            }
        }

        IEnumerator FlashGreen()
        {
            CorrectPanel.SetActive(true);
            WrongPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            CorrectPanel.SetActive(false);
            WrongPanel.SetActive(false);

            SetNewBot();
        }

        IEnumerator FlashRed(List<int> wrong)
        {
            CorrectPanel.SetActive(false);
            WrongPanel.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            CorrectPanel.SetActive(false);
            WrongPanel.SetActive(false);

            PlayerHealth--;

            ReasonPopup(wrong);
        }

        public void LoseGame()
        {
            Time.timeScale = 0;
            ReasonPanel.SetActive(false);
            StatementSelect.SetActive(false);
            OKBSMScript.LoseUI.SetActive(true);
        }

        public void LoseTrojan()
        {
            StartCoroutine(LoseTrojCor());
        }

        IEnumerator LoseTrojCor()
        {
            yield return new WaitForSeconds(.5f);

            Time.timeScale = 0;
            ReasonPanel.SetActive(false);
            StatementSelect.SetActive(false);
            OKBSMScript.TrojanLoseUI.SetActive(true);
        }

        public void Win()
        {
            Time.timeScale = 0;
            ReasonPanel.SetActive(false);
            StatementSelect.SetActive(false);
            PlayerPrefs.SetInt("Credits", 1);
            GetComponent<OKBSM>().UI.SetActive(true);
            GetComponent<OKBSM>().WinUI.SetActive(true);
            GetComponent<OKBSM>().LoseUI.SetActive(false);
            Template.SetActive(false);

        }

        void StartTrojan()
        {
            StartCoroutine(PopupBonusQn());

            StatementSelect.SetActive(false);
            ReasonPanel.SetActive(false);

            Bot.SetActive(false);
            Troy.SetActive(true);

            BotCounter.text = "Botwares: " + ((totalBots + 1).ToString() + "? / " + totalBots);

            botName.text = bc.Contents.trojan[0].name;

            statement1.text = bc.Contents.trojan[0].statements[0];
            statement2.text = bc.Contents.trojan[0].statements[1];
            statement3.text = bc.Contents.trojan[0].statements[2];

            sw.StopFly = true;
            sw.Trojan = true;

            StartCoroutine(SlideUp());
        }

        IEnumerator PopupBonusQn()
        {
            BonusQnPopup.SetActive(true);
            yield return new WaitForSeconds(2f);
            BonusQnPopup.SetActive(false);
        }
    }
}