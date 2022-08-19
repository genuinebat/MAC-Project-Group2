using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public GameObject ContinueBtn;
    public GameObject LevelSelectUI;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("NextStage"));
        if (PlayerPrefs.GetString("NextStage") == "Completed")
        {
            LevelSelectUI.SetActive(true);
        }
        if (PlayerPrefs.GetString("NextStage") != "Malorant")
        {
            ContinueBtn.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
