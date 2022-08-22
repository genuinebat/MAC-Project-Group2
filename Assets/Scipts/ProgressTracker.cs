using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public GameObject PlayButtons;
    public GameObject LevelSelectUI;
    public Button ContinueBtn;
    void OnEnable()
    {
        if (PlayerPrefs.GetString("NextStage") == "Completed")
        {
            LevelSelectUI.SetActive(true);
            PlayButtons.SetActive(false);
        }
        if (PlayerPrefs.GetString("NextStage") != "Malorant")
        {
            if (PlayerPrefs.GetString("NextStage") != "Completed")
            {
                PlayButtons.SetActive(true);
            }
            ContinueBtn.interactable = true;
        }
        else
        {
            ContinueBtn.interactable = false;

        }
    }
}
