using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Animator doors;
    public Animator door1;
    public Animator door2;

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
        StartCoroutine(NextSceneCor(PlayerPrefs.GetString("NextStage")));
    }

    IEnumerator NextSceneCor(string scenename)
    {
        door1.SetTrigger("CloseDoor");
        door2.SetTrigger("CloseDoor");

        yield return new WaitForSeconds(0.5f);

        doors.SetTrigger("RotateAntiClockwise");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(scenename);
        if (PlayerPrefs.GetString("NextStage") != "Completed")
        {
            PlayerPrefs.SetString("NextStage", scenename);
        }
    }


}