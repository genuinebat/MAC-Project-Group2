using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public GameObject ContinueBtn;
    public GameObject LevelSelectUI;
    public Button StartBtn;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("NextStage") == "Completed")
        {
            LevelSelectUI.SetActive(true);
            StartBtn.interactable = false;
            ContinueBtn.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetString("NextStage") != "Malorant")
        {
            ContinueBtn.SetActive(true);
        }
    }
}
