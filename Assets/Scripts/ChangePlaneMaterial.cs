using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaneMaterial : MonoBehaviour {

    public Material ArrowUpMaterial;
    public Material ArrowDownMaterial;

    private void OnEnable()
    {
        PlateformInstantiation.OnPlateformInstantiated += ChangeMaterial;
    }

    private void OnDisable()
    {
        PlateformInstantiation.OnPlateformInstantiated -= ChangeMaterial;
    }
    
    private void ChangeMaterial()
    {
        if (transform.GetComponentInParent<Arrows>().ArrowDirection == Arrows.Direction.Forward)
        {
            GetComponent<MeshRenderer>().material = ArrowUpMaterial;
        }
        else if(transform.GetComponentInParent<Arrows>().ArrowDirection == Arrows.Direction.Backward)
        {
            GetComponent<MeshRenderer>().material = ArrowDownMaterial;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
