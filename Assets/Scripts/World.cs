using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public Continent[] continents;
    public float totalPopulation;

    private void Awake()
    {
        continents = GetComponentsInChildren<Continent>();

    }

    private void Update()
    {
        for (int i = 0; i < continents.Length; i++)
            continents[i].updateRegions();
    }

}
