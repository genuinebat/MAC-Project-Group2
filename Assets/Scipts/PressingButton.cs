using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // boolean to check if the button is being pressed and held down
    public bool Pressing { get; private set; }
    public GameObject TutorialPanel;

    void Start()
    {
        Pressing = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressing = false;
    }
}