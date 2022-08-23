using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public GameObject PlayButtons;
    public GameObject LevelSelectUI;
    public Button ContinueBtn;

    public void Testing()
    {
        PlayerPrefs.DeleteAll();
    }

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
                LevelSelectUI.SetActive(false);
                PlayButtons.SetActive(true);
            }
            ContinueBtn.interactable = true;
        }
        else if (PlayerPrefs.GetString("NextStage") == "" || PlayerPrefs.GetString("NextStage") == null)
        {
            LevelSelectUI.SetActive(false);
            PlayButtons.SetActive(true);

            ContinueBtn.interactable = false;
        }
        else
        {
            ContinueBtn.interactable = true;
        }
    }
}
