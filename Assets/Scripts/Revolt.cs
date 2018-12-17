using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolt: MonoBehaviour {
    [System.Serializable]
    public struct RevoltParams
    {
        public Upgrade.Type type;

        public float beliversLose;
        public float amountOfFaithToDisappear;

        public string name;
        public string description;

    }
    
    public RevoltParams parameters;
}
