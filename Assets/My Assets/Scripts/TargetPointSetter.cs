using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointSetter : MonoBehaviour
{

    [SerializeField] private Camera camera;

    [SerializeField] private List<GameObject> selectedUnits;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            SetTargetPointToGroupOfUnits(mousePosition);
            /*
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Set target point to group of Unts
                Debug.Log("Set target");
                SetTargetPointToGroupOfUnits(hit.point);
            }
            */
        }
    }

    private void SetTargetPointToGroupOfUnits(Vector3 targetpoint)
    {
        foreach (GameObject unit in selectedUnits)
        {
            unit.GetComponent<MovementComponent>().SetTargetPoint(targetpoint);
        }
    }
}
