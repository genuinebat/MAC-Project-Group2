using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Animator Doors;
    public Animator Door1;
    public Animator Door2;

    public void nextScene(string scenename)
    {
        Time.timeScale = 1f;
        StartCoroutine(NextSceneCor(scenename));
    }

    public void StartNew()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("NextStage", "Malorant");
        StartCoroutine(NextSceneCor("Malorant"));
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        Debug.Log(PlayerPrefs.GetString("NextStage"));
        StartCoroutine(NextSceneCor(PlayerPrefs.GetString("NextStage")));
    }

    IEnumerator NextSceneCor(string scenename)
    {
        Door1.SetTrigger("CloseDoor");
        Door2.SetTrigger("CloseDoor");

        yield return new WaitForSeconds(0.5f);

        Doors.SetTrigger("RotateAntiClockwise");

        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.GetString("NextStage") != "Completed" || scenename != "Main")
        {
            PlayerPrefs.SetString("NextStage", scenename);
        }

        SceneManager.LoadScene(scenename);
    }
}