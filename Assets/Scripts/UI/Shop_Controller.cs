using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Controller : MonoBehaviour {

    [Header("Content")]
    public GameObject gunsTab;
    public GameObject armorTab;
    public GameObject coinsTab;

    [Header("Buttons")]
    public Image gunsButton;
    public Image armorButton;
    public Image coinsButton;
    public Sprite onSprite;
    public Sprite offSprite;

    private GameObject curTab;
    private Image curSprite;

    void Start()
    {
        curTab = gunsTab;
        curSprite = gunsButton;
    }

    public void switchTab(GameObject newTab)
    {
        curTab.SetActive(false);
        curTab = newTab;
        curTab.SetActive(true);
    }

    public void switchSprite(Image newSprite)
    {
        curSprite.sprite = offSprite;
        curSprite = newSprite;
        curSprite.sprite = onSprite;
    }
}
