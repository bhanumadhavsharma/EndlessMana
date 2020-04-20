using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class JournalManager : MonoBehaviour
{
    bool
        journalOpen;
    public GameObject
        journalUI;
    public GameObject
        costumeUI;

    public TextMeshProUGUI
        coinAmount,
        damageAmount,
        stdSpellDmg,
        iceSpellDmg,
        stdManaCost,
        iceManaCost;

    public GameObject
        redSuit,
        purpleSuit,
        greenSuit,
        blueSuit;

    public Sprite
        stdSuitSprite,
        RedSuitSprite,
        PurpleSuitSprite,
        GreenSuitSprite,
        blueSuitSprite;

    public SpriteRenderer playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        journalUI.SetActive(false);
        costumeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Select")) 
        {
            if (journalOpen)
            {
                journalUI.SetActive(false);
                journalOpen = false;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
            else
            {
                journalUI.SetActive(true);
                journalOpen = true;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }
        DisplayCoins();
        DisplayDamage();
        DisplaySpellInfo();

        playerCharacter = GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>();
    }

    void DisplayCoins()
    {
        coinAmount.text = PlayerStats.instance.coinAmount.ToString();
    }

    void DisplayDamage()
    {
        damageAmount.text = PlayerStats.instance.damage.ToString();
    }

    void DisplaySpellInfo()
    {
        stdSpellDmg.text = "+" + (PlayerStats.instance.damage + 1).ToString() + " dmg";
        iceSpellDmg.text = "+" + (PlayerStats.instance.damage).ToString() + " dmg";

        stdManaCost.text = "-" + (PlayerStats.instance.standardSpellCost) + " mana";
        iceManaCost.text = "-" + (PlayerStats.instance.iceSpellCost) + " mana";
    }

    public void MainMenu()
    {
        Debug.Log("going to main menu");
        SceneManager.LoadScene(0);
        //Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    public void CostumeButton()
    {
        journalUI.SetActive(false);
        costumeUI.SetActive(true);
        if (PlayerStats.instance.redSuitUnlocked == true)
        {
            redSuit.SetActive(true);
        } else
        {
            redSuit.SetActive(false);
        }
        if (PlayerStats.instance.purpleSuitUnlocked == true)
        {
            purpleSuit.SetActive(true);
        }
        else
        {
            purpleSuit.SetActive(false);
        }
        if (PlayerStats.instance.greenSuitUnlocked == true)
        {
            greenSuit.SetActive(true);
        }
        else
        {
            greenSuit.SetActive(false);
        }
        if (PlayerStats.instance.blueSuitUnlocked == true)
        {
            blueSuit.SetActive(true);
        }
        else
        {
            blueSuit.SetActive(false);
        }
    }

    public void PickStdCostume()
    {
        journalUI.SetActive(true);
        costumeUI.SetActive(false);
        PlayerStats.instance.ChangeCostume(1);
        playerCharacter.sprite = stdSuitSprite;
    }

    public void PickRedCostume()
    {
        journalUI.SetActive(true);
        costumeUI.SetActive(false);
        PlayerStats.instance.ChangeCostume(2);
        playerCharacter.sprite = RedSuitSprite;
    }

    public void PickPurpleCostume()
    {
        journalUI.SetActive(true);
        costumeUI.SetActive(false);
        PlayerStats.instance.ChangeCostume(3);
        playerCharacter.sprite = PurpleSuitSprite;
    }

    public void PickGreenCostume()
    {
        journalUI.SetActive(true);
        costumeUI.SetActive(false);
        PlayerStats.instance.ChangeCostume(4);
        playerCharacter.sprite = GreenSuitSprite;
    }

    public void PickBlueCostume()
    {
        journalUI.SetActive(true);
        costumeUI.SetActive(false);
        PlayerStats.instance.ChangeCostume(5);
        playerCharacter.sprite = blueSuitSprite;
    }

    /*
      public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (!XPManager.instance.levelUpWindowOpen)
        {
            Time.timeScale = 1f;
        }
        gameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //timescale control how fast time passes (used for slowmotion and stuff)
        gameIsPaused = true;
    }

    
      */
}
