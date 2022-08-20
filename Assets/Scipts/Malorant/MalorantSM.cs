using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Malorant
{
    public class MalorantSM : PuzzleSM
    {
        [Header("Reference Variables")]
        public GameObject Spawner;
        public GameObject UI;
        public GameObject Popup;
        public GameObject PopupDisplay;
        public GameObject LoseUI;
        public GameObject WinUI;
        public GameObject PauseUI;
        public GameObject ScannerUnlockUI;
        public GameObject HintTxt;
        public GameObject LockUI;
        public GameObject UnlockUI;
        public GameObject WeaponNotificationUI;
        public Button SkipButton;
        public MalorantGameState malorantState;
        int tryCount;

        [Header("Hint Text")]
        public string HintText;

        Malorant_Spawner spawnerScript;
        Dialogue dialogueScript;
        Weapons weaponScript;
        Coroutine closingCor;
        Coroutine runDialogueCor;

        Button scannerUI;
        Image scannerIcon;

        float popupHeight, popupWidth;

        void Start()
        {
            spawnerScript = Spawner.GetComponent<Malorant_Spawner>();
            scannerUI = GameObject.Find("Scanner").GetComponent<Button>();
            scannerIcon = GameObject.Find("ScannerImg").GetComponent<Image>();
            weaponScript = GetComponent<Weapons>();
            dialogueScript = GetComponent<Dialogue>();

            popupHeight = Popup.transform.localScale.y;
            popupWidth = Popup.transform.localScale.x;

            HintTxt.GetComponent<TMP_Text>().text = "hint: " + HintText;

            Cancel();
        }

        // function that is called when the image target is first detected
        // it uses a coroutine to popup a panel that allows the player to start the puzzle
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

        // function that is called if the player chooses to not start the puzzle
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

        // function that is called to start the game
        public override void Initialize()
        {
            tryCount++;
            if (IsRunning) return;

            base.Initialize();

            if (tryCount >= 2)
            {
                SkipButton.interactable = true;
            }
            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            spawnerScript.SpawnMalwares();

            HintTxt.SetActive(false);
            UI.SetActive(true);
            ScannerUnlockUI.SetActive(true);
            Popup.SetActive(false);
            Spawner.SetActive(true);
            LockUI.SetActive(true);
            UnlockUI.SetActive(true);
            WeaponNotificationUI.SetActive(false);
            PauseUI.SetActive(true);

            malorantState.GameStarted = true;

            if (runDialogueCor != null) StopCoroutine(runDialogueCor);

            runDialogueCor = StartCoroutine(dialogueScript.RunDialogue());
        }

        // function that is called to close and restart the game
        public override void Cancel()
        {
            base.Cancel();

            //reset timer for malorant
            malorantState.timeLeft = malorantState.TimeMin * 60 + malorantState.TimeSec;
            Time.timeScale = 1f;
            UI.SetActive(false);

            //lock the scanner
            scannerUI.interactable = false;
            scannerIcon.color = new Color32(255, 255, 255, 0);

            //make sure lose UI is closed
            WinUI.SetActive(false);
            LoseUI.SetActive(false);
            PauseUI.SetActive(false);
            spawnerScript.ResetMalorant();
            malorantState.GameStarted = false;

            HintTxt.SetActive(true);
        }

        public void Retry()
        {
            StartCoroutine(RetryCor());
        }

        IEnumerator RetryCor()
        {
            Cancel();
            yield return new WaitForSeconds(.1f);
            Initialize();
        }

        public void SkipGame(string TargetSceneName)
        {
            if (tryCount >= 2)
            {
                GameObject.Find("TransitionAnimator").GetComponent<ChangeScene>().nextScene(TargetSceneName);
                //SceneManager.LoadScene(TargetSceneName);
                if (PlayerPrefs.GetString("NextStage") != "Completed")
                {
                    PlayerPrefs.SetString("NextStage", TargetSceneName);
                }
            }
            else
            {

            }
        }
    }
}