using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void nextScene(string scenename)
    {
        Debug.Log("loading scene:" + scenename);
        SceneManager.LoadScene(scenename);
    }
}