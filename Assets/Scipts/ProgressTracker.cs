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
            Debug.Log("Comepleted");
            LevelSelectUI.SetActive(true);
            PlayButtons.SetActive(false);
        }
        if (PlayerPrefs.GetString("NextStage") != "Malorant")
        {
            Debug.Log("started");
            if (PlayerPrefs.GetString("NextStage") != "Completed")
            {
                Debug.Log("Ongoing");
                LevelSelectUI.SetActive(false);
                PlayButtons.SetActive(true);
            }
            ContinueBtn.interactable = true;
        }
        else
        {
            Debug.Log("not started");
            LevelSelectUI.SetActive(false);
            PlayButtons.SetActive(true);

            ContinueBtn.interactable = false;

        }
    }
}
