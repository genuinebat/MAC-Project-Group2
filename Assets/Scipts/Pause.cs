using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseUI;
    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void RestartGame()
    {
        //actual restart is using whatever sm and attaching cancel and initialize
        //this script is used only to close the UI
        PauseUI.SetActive(false);
    }
}
