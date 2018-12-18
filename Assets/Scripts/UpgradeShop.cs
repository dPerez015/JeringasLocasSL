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
    public float upgradePricePercent;
    float price;
    public Text PriceText;

    [Header("Player")]
    public PlayerManager player;

    public void Reset()
    {
        /*List<GameObject> childs = new List<GameObject>(transform.childCount);

        foreach (Transform child in transform)
            childs.Add(child.gameObject);

        for (int i = 0; i < childs.Count; i++) 
        {
            Destroy(childs[i]);
        }*/
        player = GameObject.Find("Player Manager").GetComponent<PlayerManager>();

        //change the name of the gameobject 
        gameObject.name = "Upgrade Shop";

        //generate the child gameobject to set an image do descrive the upgrade
        GameObject upgradeImageObject = new GameObject("shopSprite");
        UpgradeImageComponent = upgradeImageObject.AddComponent<Image>();
        upgradeImageObject.transform.SetParent(transform);
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

    public void AugmentPrice()
    {
        price += price * upgradePricePercent;
        PriceText.text = price.ToString("f1");
    }


    public void generateUpgrade()
    {

        //check if player has enough money to buy
        if (player.buyUpgrade(price))
        {
            GameObject upgrade = new GameObject("Upgrade " + UpgradeName);
            Image upgradeIMG = upgrade.AddComponent<Image>();
            upgradeIMG.sprite = upgradeSprite;

            Upgrade newUp = upgrade.AddComponent<Upgrade>();
            newUp.value = powerUpValue;
            newUp.shop = this;
            //change price
            AugmentPrice();

            player.currentUpgrade = upgrade;

            upgrade.transform.SetParent(transform.parent);
        }
    }

}
