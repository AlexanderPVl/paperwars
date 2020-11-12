using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackMaster : MonoBehaviour
{
    GameObject barrack;

    public void SetBarrack(GameObject go)
    {
        barrack = go;
    }

    public void PanelUnitClick(int ind)
    {
        barrack.GetComponent<BarrackComponent>().AddUnitToQueue(ind);
    }
}
