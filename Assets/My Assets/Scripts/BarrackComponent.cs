using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackComponent : MonoBehaviour
{
    UIMaster uimaster;
    [SerializeField]
    List<GameObject> unitsAllowed;
    [SerializeField]
    float delay;
    float timeToWait;
    List<int> queue;

    void Start()
    {
        timeToWait = delay;
        queue = new List<int>();
        uimaster = gameObject.transform.Find("/UIMaster").GetComponent<UIMaster>();
    }

    void Update()
    {
        if (queue.Count == 0) return;
        if (timeToWait >= 0)
            timeToWait -= Time.deltaTime;
        if (timeToWait <= 0)
        {
            CreateUnit();
            Debug.Log("time " + timeToWait.ToString());
            Debug.Log(queue.Count);
        }
    }

    void OnMouseDown()
    {
        if (SelectionMaster.IsSelectionBusy == false)
        {
            GameObject.Find("/BuildingMaster").GetComponent<BarrackMaster>().SetBarrack(gameObject);
            uimaster.SelectBottomPanel(1);
            //uimaster.PanelButtonClick(1);
        }
    }

    public void AddUnitToQueue(int ind)
    {
        queue.Add(ind);
    }

    public void CreateUnit()
    {
        if (queue == null) return;
        Debug.Log("not assigned error " + (unitsAllowed[queue[0]] == null).ToString());
        Instantiate(unitsAllowed[queue[0]].transform, gameObject.transform.position, Quaternion.identity, GameObject.Find("/Grid/Tilemap").transform);
        queue.RemoveAt(0);
        timeToWait = delay;
    }
}
