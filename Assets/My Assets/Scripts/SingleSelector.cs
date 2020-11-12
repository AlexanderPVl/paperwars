using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSelector : MonoBehaviour
{
    GameObject master;
    BoxSelector boxSelector;
    private void Start()
    {
        master = GameObject.Find("/GameMaster");
        boxSelector = master.GetComponent<BoxSelector>();
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {
        boxSelector.Deselect();
        boxSelector.SelectUnit(gameObject);
    }
}
