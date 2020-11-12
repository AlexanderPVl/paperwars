using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : MonoBehaviour
{
    // === Movement === //

    int MoveSelectState; // 0 = move, 1 = select

    CameraMovement cameraMovement;
    BoxSelector boxSelector;
    UnityEngine.UI.Text MovementSelectionButtonText;

    // === Building === //

    [SerializeField]
    List<GameObject> bottomPanels = new List<GameObject>();
    [SerializeField]
    GameObject leftPanel;
    [SerializeField]
    List<GameObject> leftPanels = new List<GameObject>();

    public enum Panels { Build = 0, Barrack = 1 };

    void Start()
    {
        MoveSelectState = 0;
        MovementSelectionButtonText = gameObject.transform.Find("/Canvas/BottomPanel/MoveSelect_Button/MVButtonText").GetComponent<UnityEngine.UI.Text>();
        cameraMovement = gameObject.transform.Find("/Main Camera").GetComponent<CameraMovement>();
        boxSelector = gameObject.transform.Find("/GameMaster").GetComponent<BoxSelector>();
    }
    public void MovementSelectButtonClick()
    {
        MoveSelectState += 1;
        MoveSelectState %= 2;

        if (MoveSelectState == 0) MovementSelectionButtonText.text = "Movement";
        if (MoveSelectState == 1) MovementSelectionButtonText.text = "Selection";

        cameraMovement.SwitchMovementAllowed();
        boxSelector.SwitchSelection();
    }
    
    public void SelectBottomPanel(int ind)
    {
        foreach (var panel in bottomPanels)
        {
            panel.SetActive(false);
        }
        bottomPanels[ind].SetActive(true);
    }

    public void ShowLeftPanel()
    {
        leftPanel.SetActive(true);
    }

    public void CloseLeftPanels()
    {
        leftPanel.SetActive(false);
    }
    public void SelectLeftPanel(int ind)
    {
        leftPanel.SetActive(true);
        foreach (var panel in leftPanels)
        {
            panel.SetActive(false);
        }
        leftPanels[ind].SetActive(true);
    }

    public bool IsSelectionModeOn()
    {
        return MoveSelectState == 1;
    }
}
