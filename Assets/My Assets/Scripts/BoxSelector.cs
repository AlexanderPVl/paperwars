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

    private Camera cam;
    void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedUnits = new List<GameObject>();
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
    }

    void Select()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);
        selectedUnits = new List<GameObject>();
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            Vector2 screenPos = cam.WorldToScreenPoint(go.transform.position);
            //Vector2 screenPos = go.transform.position;

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectedUnits.Add(go);
            }
        }
        Debug.Log("Count " + selectedUnits.Count.ToString());
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

        Select();
    }
}
