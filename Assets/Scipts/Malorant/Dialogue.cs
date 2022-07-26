using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Malorant
{
    public class Dialogue : MonoBehaviour
    {
        [Header("Dialogue Lines")]
        public List<string> Phase1;
        public List<string> Phase2;
        public List<string> Phase3;

        [Header("Reference Variables")]
        public GameObject DialogueUI;
        public GameObject WeaponsUI;
        public Image RaygunUI;
        public Image ScannerUI;

        public bool NextPhase { private get; set; }

        List<string>[] phases = new List<string>[3];

        MalorantGameState gameState;
        Weapons weaponScript;
        TMP_Text dialogueTxt;

        bool animating;

        void Start()
        {
            dialogueTxt = DialogueUI.transform.Find("DialogueUI").Find("DialoguePanel").Find("Dialogue").gameObject.GetComponent<TMP_Text>();
            gameState = GetComponent<MalorantGameState>();
            weaponScript = GetComponent<Weapons>();

            phases[0] = Phase1;
            phases[1] = Phase2;
            phases[2] = Phase3;

            DialogueUI.SetActive(false);
        }

        public IEnumerator RunDialogue()
        {
            NextPhase = true;

            foreach (List<string> phase in phases)
            {
                while (!NextPhase) yield return null;

                DialogueUI.SetActive(true);

                foreach (string dialogue in phase)
                {
                    dialogueTxt.text = "";

                    yield return StartCoroutine(AnimateDialogue(dialogue));
                    yield return StartCoroutine(WaitForNextDialogue());
                }

                if (phase == phases[0])
                {
                    weaponScript.SwitchToRaygun();
                }
                else if (phase == phases[1])
                {
                    weaponScript.SwitchToScanner();
                }

                gameState.DialogueEnd = true;

                DialogueUI.SetActive(false);

                NextPhase = false;
            }
        }

        IEnumerator AnimateDialogue(string dialogue)
        {
            animating = true;

            StartCoroutine(CheckForSkipDialogue());

            for (int i = 0; i < dialogue.Length; i++)
            {
                if (!animating)
                {
                    dialogueTxt.text = dialogue;
                    yield break;
                }

                dialogueTxt.text += dialogue[i];

                yield return new WaitForSeconds(.05f);
            }

            animating = false;
        }

        IEnumerator CheckForSkipDialogue()
        {
            yield return new WaitForSeconds(.2f);

            for (;;)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animating = false;
                    yield break;
                }
                yield return null;
            }
        }

        IEnumerator WaitForNextDialogue()
        {
            yield return new WaitForSeconds(.2f);

            for (;;)
            {
                if (Input.GetMouseButtonDown(0)) yield break;
                
                yield return null;
            }
        }

        IEnumerator FlashRaygun()
        {
            Color grey = new Color(0, 0, 0, 100);
            Color yellow = new Color(255, 255, 0, 255);

            for (int i = 0; i < 5; i++)
            {
                float a = 0f;
                for (int n = 0; n < 500; n++)
                {
                    a += Time.deltaTime / 500;

                    RaygunUI.color = Color.Lerp(yellow, grey, a);

                    yield return null;
                }

                float b = 0f;
                for (int m = 0; m < 500; m++)
                {
                    b += Time.deltaTime / 500;

                    RaygunUI.color = Color.Lerp(yellow, grey, b);

                    yield return null;
                }
                
                yield return new WaitForSeconds(.5f);
            }
        }

        IEnumerator FlashScanner()
        {
            yield return null;
        }
    }
}