using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    BoxSelector boxSelector;
    Vector3 prevMousePosition;
    Transform test;
    Camera maincamera;
    RectTransform panel;
    bool isMovementAllowed;
    private bool isCorrectButtonDown;

    void Start()
    {
        isCorrectButtonDown = false;
        panel = gameObject.transform.Find("/Canvas/BottomPanel").GetComponent<RectTransform>();
        AllowMovement();
        maincamera = Camera.main;
        prevMousePosition = Input.mousePosition;
        boxSelector = gameObject.transform.Find("/GameMaster").GetComponent<BoxSelector>();
    }
    void Update()
    {
        if (!isMovementAllowed) return;
        if (Input.GetMouseButtonDown(0) && !OnActiveScreen())
        {
            isCorrectButtonDown = false;
            return;
        }

        if (Input.GetMouseButtonDown(0) && OnActiveScreen())
        {
            isCorrectButtonDown = true;
            prevMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isCorrectButtonDown)
        {
            Vector3 delt = (maincamera.ScreenToWorldPoint(prevMousePosition) - maincamera.ScreenToWorldPoint(Input.mousePosition));
            maincamera.transform.Translate(new Vector3(delt.x, delt.y, 0));
            prevMousePosition = Input.mousePosition;
        }
    }

    public void SwitchMovementAllowed()
    {
        if (isMovementAllowed) DisableMovement();
        else AllowMovement();
    }

    public void AllowMovement()
    {
        isMovementAllowed = true;
    }

    public void DisableMovement()
    {
        isMovementAllowed = false;
    }
    public bool OnActiveScreen()
    {
        return Input.mousePosition.y > panel.sizeDelta.y;
    }
}
