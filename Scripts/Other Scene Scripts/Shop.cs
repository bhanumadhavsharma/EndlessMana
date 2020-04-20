using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI
        shopkeeperText,
        amountOfCoinsText;

    public GameObject
        redSuit,
        purpleSuit,
        greenSuit,
        blueSuit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        amountOfCoinsText.text = "Coins: " + PlayerStats.instance.coinAmount;
        if (PlayerStats.instance.redSuitUnlocked == true)
        {
            redSuit.SetActive(false);
        }
        if (PlayerStats.instance.purpleSuitUnlocked == true)
        {
            purpleSuit.SetActive(false);
        }
        if (PlayerStats.instance.greenSuitUnlocked == true)
        {
            greenSuit.SetActive(false);
        }
        if (PlayerStats.instance.blueSuitUnlocked == true)
        {
            blueSuit.SetActive(false);
        }
    }

    void DisplayShopkeeperText()
    {
        string speechText = "Welcome to my shop friend! Anything you buy here will carry over to your next life, unlike coins.";
        StartCoroutine(EffectTypewriter(speechText, shopkeeperText));
    }

    private IEnumerator EffectTypewriter(string text, TextMeshProUGUI dialog)
    {
        foreach (char character in text.ToCharArray())
        {
            dialog.text += character;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void BuyRedSuit()
    {
        if (PlayerStats.instance.coinAmount >= 50)
        {
            PlayerStats.instance.coinAmount -= 50;
            PlayerStats.instance.redSuitUnlocked = true;
        }
    }

    public void BuyPurpleSuit()
    {
        if (PlayerStats.instance.coinAmount >= 100)
        {
            PlayerStats.instance.coinAmount -= 100;
            PlayerStats.instance.purpleSuitUnlocked = true;
        }
    }

    public void BuyGreenSuit()
    {
        if (PlayerStats.instance.coinAmount >= 150)
        {
            PlayerStats.instance.coinAmount -= 150;
            PlayerStats.instance.greenSuitUnlocked = true;
        }
    }

    public void BuyBlueSuit()
    {
        if (PlayerStats.instance.coinAmount >= 200)
        {
            PlayerStats.instance.coinAmount -= 200;
            PlayerStats.instance.blueSuitUnlocked = true;
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(1);
    }
}
