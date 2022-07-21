using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public bool NextPhase { private get; set; }

        List<string>[] phases = new List<string>[3];

        MalorantGameState gameState;
        TMP_Text dialogueTxt;

        bool animating;

        void Start()
        {
            dialogueTxt = DialogueUI.transform.Find("DialoguePanel").Find("Dialogue").gameObject.GetComponent<TMP_Text>();
            gameState = GetComponent<MalorantGameState>();

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
    }
}