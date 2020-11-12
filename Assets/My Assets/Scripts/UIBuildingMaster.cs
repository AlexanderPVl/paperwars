using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingMaster : MonoBehaviour
{
    private GameObject gameMaster;
    private BuildMaster buildMaster;
    private int latestSelectedInd;
    void Start()
    {
        latestSelectedInd = -1;
        gameMaster = GameObject.Find("/GameMaster");
        buildMaster = gameMaster.GetComponent<BuildMaster>();
    }

    void Update()
    {
        
    }
    
    public void BuildingSelected(int ind)
    {
        if (ind != latestSelectedInd)
        {
            buildMaster.Deselect();
            buildMaster.SelectByInd(ind);
        }
        else
        {
            if (buildMaster.IsSelected)
            {
                buildMaster.Deselect();
            }
            else
            {
                buildMaster.SelectByInd(ind);
            }
        }
        latestSelectedInd = ind;
    }
    
    public void BuildingButtonClick()
    {

    }

}
