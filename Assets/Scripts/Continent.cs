using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continent : MonoBehaviour {

    public Region[] regions;

    public void Awake()
    {
       regions= GetComponentsInChildren<Region>();
    }

    public void updateRegions()
    {

    }

}
