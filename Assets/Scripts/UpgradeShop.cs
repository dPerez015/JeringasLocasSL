using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class UpgradeShop : MonoBehaviour {

    //values to set image component 
    [Header("Shop image")]
    public Sprite shopImage;
    public Image UpgradeImageComponent;

    [Header("Upgrade parameters")]
    public Sprite upgradeSprite;
    public Text UpgradeNameText;
    public string UpgradeName="";

    [Header("PowerUp Value")]
    public float powerUpValue;
    public Text PowerText;

    [Header("Prizes")]
    public float initialPrice;
    float price;
    public Text PriceText;

    public void Reset()
    {
        //change the name of the gameobject 
        gameObject.name = "Upgrade Shop";

        //generate the child gameobject to set an image do descrive the upgrade
        GameObject upgradeImageObject = new GameObject("shopSprite");
        UpgradeImageComponent = upgradeImageObject.AddComponent<Image>();
        upgradeImageObject.transform.SetParent(gameObject.transform);
        UpgradeImageComponent.rectTransform.position = transform.position;
        
    }

    public void OnValidate()
    {
        if(shopImage!=null)
            UpgradeImageComponent.sprite = shopImage;
        if(UpgradeNameText != null)
            UpgradeNameText.text = UpgradeName;
        if (PowerText != null)
            PowerText.text = powerUpValue.ToString();
        if (PriceText != null)
            PriceText.text = initialPrice.ToString();

    }

    public void Start()
    {
        price = initialPrice;
    }


    public void generateUpgrade()
    {
        GameObject upgrade = new GameObject("Upgrade " + UpgradeName);
        Image upgradeIMG = upgrade.AddComponent<Image>();
        upgradeIMG.sprite = upgradeSprite;
        upgradeIMG.raycastTarget = false;

        upgrade.AddComponent<Upgrade>();

        upgrade.transform.SetParent(transform.parent);
    }

}
