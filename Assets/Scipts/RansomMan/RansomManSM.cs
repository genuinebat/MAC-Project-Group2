using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;

namespace RansomMan
{
    public class RansomManSM : PuzzleSM
    {
        [Header("Reference Variables")]
        public GameObject Popup;
        public GameObject PopupDisplay;
        public GameObject HintTxt;
        public GameObject UI;
        public GameObject WinUI;
        public GameObject LoseUI;
        public GameObject Player;
        public RansomwareSpawner Spawner;
        public NodeManager nm;

        [Header("Hint Text")]
        public string HintText;

        Coroutine closingCor;

        float popupHeight, popupWidth;

        void Start()
        {
            popupHeight = Popup.transform.localScale.y;
            popupWidth = Popup.transform.localScale.x;

            HintTxt.GetComponent<TMP_Text>().text = "hint: " + HintText;

            Cancel();

            // FOR DEVLOPMENT ONLY
            //StartCoroutine(LateStart());
        }

        // FOR DEVELOPMENT ONLY
        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(3f);
            Initialize();
        }

        public override void EnablePopup()
        {
            if (IsRunning || IsCompleted) return;

            base.EnablePopup();

            if (closingCor != null) StopCoroutine(closingCor);

            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            PopupDisplay.SetActive(false);
            Popup.SetActive(true);

            StartCoroutine(OpenPopup());
        }

        public override void DisablePopup()
        {
            closingCor = StartCoroutine(ClosePopup());
        }

        // coroutine for the opening popup animation
        IEnumerator OpenPopup()
        {
            // expanding the popup horizontally
            while (Popup.transform.localScale.x < (popupWidth - 0.02f))
            {
                Popup.transform.localScale =
                    Vector3.Lerp(
                        Popup.transform.localScale,
                        new Vector3(
                            popupWidth,
                            Popup.transform.localScale.y,
                            Popup.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            Popup.transform.localScale = new Vector3(popupWidth, Popup.transform.localScale.y, Popup.transform.localScale.z);

            yield return new WaitForSeconds(.1f);

            // expanding the popup vertically
            while (Popup.transform.localScale.y < (popupHeight - 0.02f))
            {
                Popup.transform.localScale =
                    Vector3.Lerp(
                        Popup.transform.localScale,
                        new Vector3(
                            popupWidth,
                            popupHeight,
                            Popup.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            Popup.transform.localScale = new Vector3(popupWidth, popupHeight, Popup.transform.localScale.z);

            PopupDisplay.SetActive(true);
        }

        // coroutine for the closing popup animation
        IEnumerator ClosePopup()
        {
            PopupDisplay.SetActive(false);

            // closing the popup vertically
            while (Popup.transform.localScale.y > (0.1f + 0.02f))
            {
                Popup.transform.localScale =
                    Vector3.Lerp(
                        Popup.transform.localScale,
                        new Vector3(
                            Popup.transform.localScale.x,
                            0.1f,
                            Popup.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            Popup.transform.localScale = new Vector3(Popup.transform.localScale.x, 0.1f, Popup.transform.localScale.z);

            yield return new WaitForSeconds(.1f);

            // closing the popup horizontally
            while (Popup.transform.localScale.x > 0.02f)
            {
                Popup.transform.localScale =
                    Vector3.Lerp(
                        Popup.transform.localScale,
                        new Vector3(
                            0f,
                            0.1f,
                            Popup.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            closingCor = null;

            Popup.SetActive(false);
        }

        public override void Initialize()
        {
            if (IsRunning) return;

            base.Initialize();

            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            HintTxt.SetActive(false);
            UI.SetActive(true);

            nm.SpawnMapPrefabs();

            PlayerMovement pm = Player.GetComponent<PlayerMovement>();

            pm.PlayerStartPosition();
            pm.GameStarted = true;

            Spawner.SpawnRansomwares();

            GetComponent<ByteTracker>().GameStarted = true;
        }

        public override void Cancel()
        {
            base.Cancel();

            Time.timeScale = 1f;

            UI.SetActive(false);
            HintTxt.SetActive(true);
            WinUI.SetActive(false);
            LoseUI.SetActive(false);
            Player.transform.position = new Vector3(100, 100, 100);

            // reset nodemanager, ransomwares, player and turn off all gameobject associated with it
        }
    }
}