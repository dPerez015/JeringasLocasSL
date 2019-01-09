using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continent : MonoBehaviour {

    public Region[] regions;


    public void Awake()
    {
       regions = GetComponentsInChildren<Region>();
    }

    public void updateRegions()
    {
        foreach (Region region in regions)
            region.updatePopulation();
    }

    public void activate()
    {
        foreach (Region region in regions)
            region.activate();
    }

    public void deactivate()
    {
        foreach (Region region in regions)
            region.deactivate();
    }

    public int getBelivers()
    {
        float belivers = 0;
        foreach (Region reg in regions)
            belivers += reg.actualBelivers;
        return (int)Mathf.Floor(belivers);
    }

    public float getPopulation()
    {
        float population = 0;
        foreach(Region reg in regions)
            population += reg.totalPopulation;
        return population;
    }

    public float getGrowth()
    {
        float growth = 0;
        foreach (Region reg in regions)
            growth += reg.getGrowth();
        return growth;
    }

}
