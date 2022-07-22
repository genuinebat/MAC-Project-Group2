using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Malorant
{
    public class MalorantGameState : MonoBehaviour
    {
        [Header("Reference Variables")]
        public GameObject LoseUI;
        public GameObject WinUI;
        public TMP_Text TimerUI;

        [Header("Functionality Variables")]
        public float TimeMin;
        public float TimeSec;

        [HideInInspector]
        public GameObject Enemies;

        [HideInInspector]
        public bool GameStarted;

        [HideInInspector]
        public float timeLeft;

        public bool DialogueEnd { private get; set;}

        Dialogue dialogueScript;

        bool won;

        void Start()
        {
            Enemies = GameObject.Find("Spawner");
            dialogueScript = GetComponent<Dialogue>();

            timeLeft = TimeMin * 60 + TimeSec;
            GameStarted = false;
            DialogueEnd = false;
            won = false;

            StartCoroutine(FlashRed());
            StartCoroutine(Pulse());
        }

        void Update()
        {
            // checking if the game has started already
            if (GameStarted == true)
            {
                if (!won) timeLeft -= Time.deltaTime;

                string minutes = ((int)timeLeft / 60).ToString("00");
                string seconds = Mathf.Round(timeLeft % 60).ToString("00");

                TimerUI.text = minutes + ":" + seconds;

                if (timeLeft < 0)
                {
                    timeLeft = 0;
                    Time.timeScale = 0;

                    LoseUI.SetActive(true);
                }
            }
        }

        // function to check if all of the malwares have been destroyed
        public void CheckWin()
        {
            if (Enemies.transform.childCount == 2)
            {
                Transform trojan = Enemies.transform.Find("Trojan(Clone)");

                if (trojan != null)
                {
                    trojan.Find("SpeechBubble").gameObject.SetActive(true);
                    trojan.gameObject.tag = "ScannableEnemy";
                }
            }

            if (Enemies.transform.childCount <= 1)
            {
                won = true;
                StartCoroutine(WaitForDialogue());
            }
        }

        IEnumerator WaitForDialogue()
        {
            DialogueEnd = false;
            dialogueScript.NextPhase = true;

            for (;;)
            {
                if (DialogueEnd)
                {
                    WinUI.SetActive(true);
                    yield break;
                }

                yield return null;
            }
        }

        IEnumerator FlashRed()
        {
            for (;;)
            {
                yield return new WaitForSeconds(.5f);
                TimerUI.color = Color.red;

                yield return new WaitForSeconds(.5f);
                TimerUI.color = Color.white;
            }
        }

        IEnumerator Pulse()
        {
            for (;;)
            {
                for (int i = 0; i < 20; i++)
                {
                    TimerUI.fontSize += 1;
                    yield return new WaitForSeconds(.05f);
                }
                
                for (int i = 0; i < 20; i++)
                {
                    TimerUI.fontSize -= 1;
                    yield return new WaitForSeconds(.05f);
                }
            }
        }
    }

}