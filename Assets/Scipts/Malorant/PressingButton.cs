using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool Pressing { get; private set; }

    void Start()
    {
        Pressing = false;
    }

    public void OnPointerDown(PointerEventData eventData){
        Pressing = true;
    }
    
    public void OnPointerUp(PointerEventData eventData){
        Pressing = false;
    }
}