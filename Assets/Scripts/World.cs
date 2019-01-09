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

    public void updateContinents()
    {
        for (int i = 0; i < continents.Length; i++)
            continents[i].updateRegions();
    }

    public float getGrowth()
    {
        float growth = 0;
        foreach (Continent cont in continents)
            growth+=cont.getGrowth();
        return growth;
    }

    public int getBelivers()
    {
        float belivers = 0;
        foreach (Continent cont in continents)
            belivers += cont.getBelivers();
        return (int)belivers;
    }

    public float getPopulation()
    {
        float population = 0;
        foreach (Continent cont in continents)
            population += cont.getPopulation();
        return Mathf.Floor(population);
    }

    public float getPercentConverted()
    {
        return getBelivers() /getPopulation();
    }

}
