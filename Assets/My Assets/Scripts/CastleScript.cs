using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleScript : MonoBehaviour
{
    UIMaster uimaster;

    void Start()
    {
        uimaster = GameObject.Find("UIMaster").GetComponent<UIMaster>();
    }
    void OnMouseDown()
    {
        Debug.Log("MouseDown works");
        uimaster.SelectLeftPanel(0);
    }
}
