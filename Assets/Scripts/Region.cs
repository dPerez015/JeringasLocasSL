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
    float population;
    public float totalPopulation;
    public float beliversGrowthPS;
    public float actualBelivers;

    public float SocialMediaImportance;
    public float PublicityImportance;
    public float GovernmentImportance;

    PlayerManager player;

    [System.NonSerialized]
    public Continent continent;

    public void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        continent = transform.parent.GetComponent<Continent>();
    }


    public void updatePopulation()
    {
        actualBelivers += beliversGrowthPS * Time.deltaTime;
    }

    public void getUpgrade(Upgrade up)
    {
        beliversGrowthPS += up.value;
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
