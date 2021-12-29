using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int Coins;
    private string BuySkins;
    private bool buying = false;
    private string Skin;

    private void Start()
    {
        Coins = PlayerPrefs.GetInt("Coin");
        BuySkins = PlayerPrefs.GetString("BuySkin");
        Skin = PlayerPrefs.GetString("Skin");

        string[] BuySkinsA = BuySkins.Split(' ');
        for (int i = 0; i < BuySkinsA.Length; i++)
        {
            transform.Find("Canvas").Find("ToggleGroup").Find(BuySkinsA[i]).Find("Buy").gameObject.SetActive(false);
        }
        
        transform.Find("Canvas").Find("ToggleGroup").Find(Skin).GetComponent<Toggle>().isOn = true;
    }

    public void SkinShop0(int price)
    {
        if (Coins >= price)
        {
            Coins -= price;
            PlayerPrefs.SetInt("Coin", Coins);
            buying = true;
        }
    }

    public void SkinShop1(string skin)
    {
        if (buying)
        {
            transform.Find("Canvas").Find("ToggleGroup").Find(skin).Find("Buy").gameObject.SetActive(false);
            PlayerPrefs.SetString("BuySkin", BuySkins + ' ' + skin);
            PlayerPrefs.SetString("Skin", skin);
            transform.Find("Canvas").Find("ToggleGroup").Find(skin).GetComponent<Toggle>().isOn = true;
            buying = false;
        }        
    }
}
