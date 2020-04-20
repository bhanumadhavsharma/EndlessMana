using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI
        healthText,
        manaText,
        xpText,
        xpLevelText;
    [SerializeField]
    private Image
        healthBar,
        manaBar,
        xpBar;

    private GameObject 
        GUI;
    private LevelGeneration 
        LG;


    private float 
        currentHealth, 
        maxHealth, 
        currentMana, 
        maxMana, 
        currentXP, 
        XPToNextLevel,
        level;

    // Start is called before the first frame update
    void Start()
    {
        LG = GameObject.Find("LevelGeneration").GetComponent<LevelGeneration>();
        GUI = GameObject.Find("GameUI");
        GUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //PS = GameObject.Find("player").GetComponent<PlayerStats>();
        //CheckIfLevelCreated();
        DisplayUI();
        PlayerStats.instance.RefillMana();
    }

    void CheckIfLevelCreated()
    {
        if (LG.stopGeneration == true)
        {
            GUI.SetActive(true);
        }
    }

    public void DisplayUI()
    {
        DisplayHealth();
        DisplayMana();
        DisplayXP();
    }

    void DisplayHealth()
    {
        currentHealth = PlayerStats.instance.health;
        maxHealth = PlayerStats.instance.maxHealth;
        healthText.text = (int)currentHealth + " / " + (int)maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void DisplayMana()
    {
        currentMana = PlayerStats.instance.mana;
        maxMana = PlayerStats.instance.maxMana;
        manaText.text = (int)currentMana + " / " + (int)maxMana;
        manaBar.fillAmount = currentMana / maxMana;
    }

    void DisplayXP()
    {
        currentXP = (float)PlayerStats.instance.xp;
        XPToNextLevel = (float)PlayerStats.instance.xpToNextLevel;
        level = PlayerStats.instance.level;
        xpBar.fillAmount = (float) currentXP / XPToNextLevel;
        xpText.text = currentXP + " / " + XPToNextLevel;
        xpLevelText.text = level.ToString();
    }
}
