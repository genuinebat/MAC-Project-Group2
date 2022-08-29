using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public Button P2, P3, P4;
    public GameObject CBtn;

    void Start()
    {
        P2.interactable = PlayerPrefs.GetInt("Puzzle2") == 1 ? true : false;
        P3.interactable = PlayerPrefs.GetInt("Puzzle3") == 1 ? true : false;
        P4.interactable = PlayerPrefs.GetInt("Puzzle4") == 1 ? true : false;


        if (PlayerPrefs.GetInt("Credits") == 1)
        {
            //P4.interactable = true;
            CBtn.SetActive(true);
        }
        else
        {
            //P4.interactable = false;
            CBtn.SetActive(false);
        }
    }
}
