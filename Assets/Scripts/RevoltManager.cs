using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevoltManager : MonoBehaviour {

    Revolt[] types;
    public AnimationCurve curveOfProb;

    public void Reset()
    {
        gameObject.name = "RevoltManager";
    }

    public void Awake()
    {
       
        types = gameObject.GetComponentsInChildren<Revolt>();
    }
    
    public float getProb(float time,float max)
    {
        return max * curveOfProb.Evaluate(time);
    }

    public Revolt getRevoltType()
    {
        
       return types[Random.Range(0, types.Length)];
    }
}
