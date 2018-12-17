using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(BoxCollider2D))]
public class Region : MonoBehaviour {
    
    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite highlightSprite;

    SpriteRenderer rend;

    [Header("Parameters")]
    public float totalPopulation;
    public float beliversGrowthPS;
    public float actualBelivers;
    float population;

    [Header("UpgradesImportance")]
    public float SocialMediaImportance;
    public float PublicityImportance;
    public float GovernmentImportance;

    PlayerManager player;

    [Header("Revolts")]
    public float maxProbOfApearence;
    float actualRevoltProb;
    public float timeBetweenChecks;
    float timeSinceLastCheck = 0;
    RevoltManager revoltManager;
    Revolt.RevoltParams currentRevolt;
    bool revoltOnGoing;

    public Image alert;
    public Text type;


    [System.NonSerialized]
    public Continent continent;

    public void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        continent = transform.parent.GetComponent<Continent>();
    }

    public void Start()
    {
        revoltManager = GameObject.Find("RevoltManager").GetComponent<RevoltManager>();
    }

    public void activateRevolt()
    {
        revoltOnGoing = true;
    }


    public void updatePopulation()
    {
        actualBelivers = Mathf.Clamp(actualBelivers + beliversGrowthPS * Time.deltaTime,0,totalPopulation);

        //check for the revolts
        timeSinceLastCheck += Time.deltaTime;
        if (timeSinceLastCheck>timeBetweenChecks)
        {
            //get a revolt
            currentRevolt = revoltManager.getRevoltType().parameters;
            activateRevolt();
            timeSinceLastCheck -= timeBetweenChecks;
           
        }
    }

    float convertUpgradeValue(float initVal, Upgrade.Type type)
    {
        float value;
        switch (type)
        {
            case Upgrade.Type.Government:
                value = initVal * GovernmentImportance;
                break;
            case Upgrade.Type.Public:
                value = initVal * PublicityImportance;
                break;
            case Upgrade.Type.Social:
                value = initVal * SocialMediaImportance;
                break;
            default:
                value = 0;
                break;
        }
        return value;
    }

    public void getUpgrade(Upgrade up)
    {
        if (revoltOnGoing)
            beliversGrowthPS += convertUpgradeValue(up.value, up.type);
        else
        {
            currentRevolt.amountOfFaithToDisappear -= convertUpgradeValue(up.value, up.type);
            //change text
            if (currentRevolt.amountOfFaithToDisappear <= 0)
            {
                revoltOnGoing = false;
            }
        }

    }

    public float getGrowth()
    {
        return Mathf.Min(beliversGrowthPS,totalPopulation-actualBelivers);
    }

    public void activate()
    {
        rend.sprite = highlightSprite;
    }

    public void deactivate()
    {
        rend.sprite = normalSprite;
    }


}
