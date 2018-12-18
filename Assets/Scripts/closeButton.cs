using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeButton : MonoBehaviour {
    public GameObject menu;

    public void closeMenu()
    {
        menu.SetActive(false);
    }
}
