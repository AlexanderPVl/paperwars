using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionMaster
{
    static bool isSelectionBusy = false;
    public static bool IsSelectionBusy => isSelectionBusy;
    public static string occupyingClass = null;

    static RectTransform bottomPanel; // = GameObject.Find("/Canvas/BottomPanel").GetComponent<RectTransform>();
    static RectTransform leftPanel; // = GameObject.Find("/Canvas/LeftPanel").GetComponent<RectTransform>();
    public static void OccupySelection(string className)
    {
        occupyingClass = className;
        isSelectionBusy = true;
    }

    public static void ReleaseSelection()
    {
        isSelectionBusy = false;
        occupyingClass = null;
    }

    public static bool OnActiveScreen()
    {
        if (bottomPanel != null)
            if (Input.mousePosition.y <= bottomPanel.sizeDelta.y && IsPanelActive("bottom") == true)
                return false;
        if (leftPanel != null)
            if (Input.mousePosition.x <= leftPanel.sizeDelta.x && IsPanelActive("left") == true)
                return false;
        return true;
    }

    public static bool IsPanelActive(string type) // type = left, bottom, ...
    {
        switch (type)
        {
            case "bottom":
                return bottomPanel.gameObject.activeInHierarchy;
            case "left":
                return leftPanel.gameObject.activeInHierarchy;
            default:
                return false;
        }
    }

    public static void InitializePanels()
    {
        foreach (var obj in Resources.FindObjectsOfTypeAll<RectTransform>())
        {
            if (obj.name == "BottomPanel")
                bottomPanel = obj;
            if (obj.name == "LeftPanel")
                leftPanel = obj;
        }
        if (bottomPanel == null || leftPanel == null)
            Debug.Log("Panels initialization error.");
    }
}
