using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

    enum Type {Social, Public, Government};
    enum Scale {Continent, Region};
    public float value;
    public UpgradeShop shop;




    // Update is called once per frame
    void Update () {
        transform.position = Input.mousePosition;
	}

}
