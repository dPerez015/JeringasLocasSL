using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsTab : MonoBehaviour {

    public Transform initialPos;
    public Transform finalPos;

    public AnimationCurve animCurve;
    public float animTime;

    float initialTime;

    Vector3 objective;
    bool animationPlaying;
    bool isBeingShown;

    public GameObject alert;
    public void Start()
    {
        isBeingShown = false;
        animationPlaying = false;
    }

    public void showOrHide() {
        if (isBeingShown)
            objective = initialPos.position;
        else
            objective = finalPos.position;

        animationPlaying = true;
        initialTime = 0;
        alert.SetActive(true);
    }


	// Update is called once per frame
	public void Update () {
        if (animationPlaying)
        {
            //initial time augment
            initialTime += Time.deltaTime;

            //move object
            if (!isBeingShown)
                transform.position = Vector3.Lerp(initialPos.position, finalPos.position, animCurve.Evaluate(initialTime / animTime));
            else
                transform.position = Vector3.Lerp(finalPos.position, initialPos.position, animCurve.Evaluate(initialTime / animTime));

            //check animation ended
            if (initialTime >= animTime)
            {
                animationPlaying = false;
                isBeingShown = !isBeingShown;
            }
            
        }
        
		
	}
}
