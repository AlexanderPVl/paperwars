using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelector : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectionBox;
    private Vector2 startPos;
    private Vector2 curMousePos;
    private List<GameObject> selectedUnits = new List<GameObject>();
    private bool anySelected;
    private UIMaster uimaster;
    public bool AnySelected => anySelected;
    public List<GameObject> SelectedUnits => selectedUnits;

    private bool isSelectionTurnedOn;
    private bool isSelectionStarted;

    private Camera cam;
    void Awake()
    {
        uimaster = GameObject.Find("/UIMaster").GetComponent<UIMaster>();
        isSelectionStarted = false;
        DisableSelection();
        isSelectionTurnedOn = false;
        anySelected = false;
        selectedUnits = new List<GameObject>();
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Deselect();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            if (IsSelectionAllowed())
            {
                startPos = Input.mousePosition;
                isSelectionStarted = true;
            }
            else isSelectionStarted = false;
        }

        if (!IsSelectionAllowed())
        {
            ReleaseSelectionBox();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (isSelectionStarted == false) return;
            UpdateSelectionBox(Input.mousePosition);
        }
    }

    private bool IsSelectionAllowed()
    {
        return (isSelectionTurnedOn == true) && (SelectionMaster.IsSelectionBusy == false) && SelectionMaster.OnActiveScreen() && uimaster.IsSelectionModeOn() == true;
    }

    private void AllowSelection()
    {
        isSelectionTurnedOn = true;
    }

    private void DisableSelection()
    {
        Deselect();
        isSelectionTurnedOn = false;
    }

    public void SwitchSelection()
    {
        if (isSelectionTurnedOn) DisableSelection();
        else AllowSelection();
    }

    void Select()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Unit"))
        {
            Vector2 screenPos = cam.WorldToScreenPoint(go.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectedUnits.Add(go);
                go.transform.Find("sprite").gameObject.SetActive(true);
            }
        }

        if (selectedUnits != null) anySelected = true;
    }

    public void SelectUnit(GameObject go)
    {
        selectedUnits.Add(go);
        go.transform.Find("sprite").gameObject.SetActive(true);
        anySelected = true;
    }

    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        Select();
    }

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }
    public void Deselect()
    {
        anySelected = false;
        foreach (var u in selectedUnits)
        {
            u.transform.Find("sprite").gameObject.SetActive(false);
        }
        selectedUnits = new List<GameObject>();
    }

    public int CntUnitsSelected()
    {
        return selectedUnits.Count;
    }
}
