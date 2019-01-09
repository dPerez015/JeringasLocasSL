using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Sprite))]
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
    public float maxProbOfRevolt;
    float actualRevoltProb;
    public float timeBetweenChecks;
    float timeSinceLastCheck = 0;
    RevoltManager revoltManager;
    Revolt.RevoltParams currentRevolt;
    bool revoltOnGoing;

    public Image alert;
    public Text typeText;
    public Text value;


    [System.NonSerialized]
    public Continent continent;


    public Color normalColor;
    public Color conqueredColor;


    //initialization
    public void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        continent = transform.parent.GetComponent<Continent>();
    }

    public void Start()
    {
        revoltManager = GameObject.Find("RevoltManager").GetComponent<RevoltManager>();

        timeSinceLastCheck = 0;
    }

    //revolt
    void activateRevolt()
    {
        currentRevolt = revoltManager.getRevoltType().parameters;
        revoltOnGoing = true;
        alert.gameObject.SetActive(true);
        value.text = currentRevolt.amountOfFaithToDisappear.ToString("f1");
        string type="";
        switch (currentRevolt.type)
        {
            case Upgrade.Type.Government:
                type = "Government";
                break;
            case Upgrade.Type.Public:
                type = "Public Opinion";
                break;
            case Upgrade.Type.Social:
                type = "Social Media";
                break;
        }
        //type = currentRevolt.name;
        typeText.text = type;
    }
    void deactivateRevolt()
    {
        revoltOnGoing = false;
        alert.gameObject.SetActive(false);
        beliversGrowthPS /= 3;
    }

    void tryRevolt()
    {
        int _try = Random.Range(0, 101);
        float probability=revoltManager.getProb(actualBelivers/totalPopulation,maxProbOfRevolt);
        if (_try < probability)
        {
            //get a revolt
            activateRevolt();
        }
    }

    //update
    public void updatePopulation()
    {
        if (!revoltOnGoing)
        {
            actualBelivers = Mathf.Clamp(actualBelivers + beliversGrowthPS * Time.deltaTime, 0, totalPopulation);
            //check for the revolts
            timeSinceLastCheck += Time.deltaTime;
            if (timeSinceLastCheck > timeBetweenChecks)
            {
                tryRevolt();
                timeSinceLastCheck -= timeBetweenChecks;
            }
        }
        else
            actualBelivers = Mathf.Clamp(actualBelivers - currentRevolt.beliversLose * Time.deltaTime, 0, totalPopulation);

        //change color
        rend.color = Color.Lerp(normalColor, conqueredColor, actualBelivers / totalPopulation);
    }


    //parameters getter
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
        if (!revoltOnGoing)
            beliversGrowthPS += convertUpgradeValue(up.value, up.type);
        else
        {
            currentRevolt.amountOfFaithToDisappear -= convertUpgradeValue(up.value, up.type);
            //change text

            value.text = currentRevolt.amountOfFaithToDisappear.ToString("f1");
            //check if ended
            if (currentRevolt.amountOfFaithToDisappear <= 0)
            {
                deactivateRevolt();
            }
        }

    }

    //parameters getter
    public float getGrowth()
    {
        return Mathf.Min(beliversGrowthPS,totalPopulation-actualBelivers);
    }


    //activation
    public void activate()
    {
        rend.sprite = highlightSprite;
    }

    public void deactivate()
    {
        rend.sprite = normalSprite;
    }


}
