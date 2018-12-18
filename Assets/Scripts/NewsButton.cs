using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsButton : MonoBehaviour {

    public Image showImage;
    public Sprite NewsSprite;
    public GameObject showControls;
    public NewsTab newstab;

    public void showNews()
    {
        showImage.sprite = NewsSprite;
        showControls.SetActive(true);
        newstab.showOrHide();
    }
}
