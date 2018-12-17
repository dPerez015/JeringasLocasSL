using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevoltManager : MonoBehaviour {

    Revolt[] types;

    public void Awake()
    {
        gameObject.name = "RevoltManager";
        types = gameObject.GetComponentsInChildren<Revolt>();
    }

    public Revolt getRevoltType()
    {
       return types[Random.Range(0, types.Length)];
    }
}
