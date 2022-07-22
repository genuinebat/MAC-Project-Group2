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
        StartCoroutine(NextSceneCor(scenename));
    }

    IEnumerator NextSceneCor(string scenename)
    {
        door1.SetTrigger("CloseDoor");
        door2.SetTrigger("CloseDoor");

        yield return new WaitForSeconds(1f);

        doors.SetTrigger("RotateAntiClockwise");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scenename);
    }
}