using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSM : MonoBehaviour
{
    public enum State { NotCompleted, Running, Completed }
    State currentState;

    public bool IsRunning
    {
        get
        {
            return currentState == State.Running ? true : false;
        }
    }
    public bool IsCompleted
    {
        get
        {
            return currentState == State.Completed ? true : false;
        }
    }

    public PuzzleSM()
    {
        currentState = State.NotCompleted;
    }

    public virtual void StartPuzzle()
    {

    }

    public virtual void EnablePopup(GameObject PopupTemp, GameObject DisplayTemp)
    {

    }

    public virtual void DisablePopup(GameObject PopupTemp, GameObject DisplayTemp)
    {

    }

    public virtual void Initialize()
    {
        currentState = State.Running;
    }

    public virtual void Cancel()
    {
        currentState = State.NotCompleted;
    }
}