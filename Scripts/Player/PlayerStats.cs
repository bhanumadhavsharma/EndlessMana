using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private GameManager GM;

    private int
        count1Damage,
        count2Damage,
        countManaRefill;
    public int
        gamelevel;

    public float
        maxHealth = 10,
        health,
        maxMana = 10,
        mana,
        refillMana = 2,
        moveSpeed = 5f;

    public int
        xp,
        xpToNextLevel = 10,
        level,
        selectedSpell = 1,
        totalSpells = 1,
        standardSpellCost,
        iceSpellCost,
        fireSpellCost,
        forceSpellCost;

    public int
        damage;

    public bool
        levelupuiActive,
        simpleMapsActive;
    public int
        healthUpBy,
        ManaUpBy,
        DamageUpBy,
        ManaRegenUpBy;
    public TextMeshProUGUI
        healthInfo,
        ManaInfo,
        DamageInfo,
        ManaRegenInfo;
    public GameObject
        ManaRegenUI;

    private GameObject
        player;
    public GameObject
        levelUpUI;
    public GameObject
        GameUI;

    public JournalManager
        JM;

    public int
        coinAmount;

    public bool 
        standardSuitUnlocked = true,
        blueSuitUnlocked = false,
        greenSuitUnlocked = false,
        redSuitUnlocked = false,
        purpleSuitUnlocked = false;

    public static PlayerStats instance;

    /// <summary>
    /// MAIN FUNCTIONS
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SetStdStats();
        InitialLoadStats();
        DisableLevelUpPanel();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        JM = transform.Find("UI").gameObject.GetComponent<JournalManager>();
        SetStdStats();
        InitialLoadStats();
    }

    void Update()
    {
        player = GameObject.Find("Player(Clone)");

        //GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        /*healthInfo = GameObject.Find("GameManager").transform.Find("LevelUpUI").transform.Find("Info1").GetComponent<TextMeshProUGUI>();
        ManaInfo = GameObject.Find("GameManager/LevelUpUI/Info2").GetComponent<TextMeshProUGUI>();
        DamageInfo = GameObject.Find("GameManager/LevelUpUI/Info3").GetComponent<TextMeshProUGUI>();
        ManaRegenInfo = GameObject.Find("GameManager/LevelUpUI/Info4").GetComponent<TextMeshProUGUI>();
        ManaRegenUI = GameObject.Find("GameManager/LevelUpUI/Info4");
        levelUpUI = GameObject.Find("GameManager/LevelUpUI"); */
        GameUI = GameObject.Find("/GameManager/GameUI").gameObject;
//        GameUI = GameObject.FindWithTag("GameUI").gameObject;
    }

    /// <summary>
    /// SECONDARY FUNCTIONS
    /// </summary>

    void InitialLoadStats()
    {
        health = maxHealth;
        mana = maxMana;
    }

    void SetStdStats()
    {
        maxHealth = 10 + (2 * (level - 1));
        maxMana = 10 + (2 * (level - 1));
        refillMana = 2 + (2 * countManaRefill);
        damage = (1 * count1Damage) + (2 * count2Damage);
        moveSpeed = 5f;
    }

    public void ChangeCostume(int costumeNumber)
    {
        switch (costumeNumber)
        {
            case 1:
                SetStdStats();
                break;
            case 2:
                SetStdStats();
                maxHealth += 10;
                break;
            case 3:
                SetStdStats();
                damage += 5;
                //increased damage
                break;
            case 4:
                SetStdStats();
                moveSpeed = 8f;
                //faster walking speed
                break;
            case 5:
                SetStdStats();
                refillMana += 3;
                //faster mana regen
                break;
        }
    }

    /// <summary>
    /// SPELL FUNCTIONS
    /// </summary>

    public void UseSpell(int amount)
    {
        mana -= amount;
    }

    public void RefillMana()
    {
        mana += refillMana * Time.deltaTime;
        mana = Mathf.Clamp(mana, 0f, maxMana);
    }

    /// <summary>
    /// HEALTH FUNCTIONS
    /// </summary>

    public void OnHit(int effect)
    {
        switch (effect)
        {
            case 1:
                DecreaseHealth(1); //enemy touch
                break;
            case 2:
                DecreaseHealth(2); //enemy fireball
                break;
            case 3:
                DecreaseHealth(5); //enemy lvl5 touch
                break;
            case 4:
                DecreaseHealth(10); //enemy lvl5 fireball
                break;
            case 5:
                DecreaseHealth(15); //enemy lvl10 touch
                break;
            case 6:
                DecreaseHealth(20); //enemy lvl10 fireball
                break;
            case 7:
                DecreaseHealth(25); //enemy lvl15 touch
                break;
            case 8:
                DecreaseHealth(30); //enemy lvl15 fireball
                break;
        }
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(player);
        count1Damage = 0;
        count2Damage = 0;
        countManaRefill = 0;
        xpToNextLevel = 10;
        level = 1;
        xp = 0;
        SetStdStats();
        InitialLoadStats();
        coinAmount = 0;
        GameOptions.instance.gameLevelNumber = 1;

        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// XP FUNCTIONS
    /// </summary>

    public void AddXP(int amount)
    {
        xp += amount;                       //xp = 0 + 27 = 27          //xp =  
        while (xp >= xpToNextLevel)         //if (27 >= 10) TRUE
        {
            level++;                        //level 1 -> 2
            xp -= xpToNextLevel;            //27 - 10 = 17
            SetXPToNextLevel(level + 1);    //next xp needed: 16 XP

            if (level % 5 == 0)
            {
                ManaRegenUpBy = 2;
                refillMana += 2;
                countManaRefill++;
            }
            if (level < 10)
            {
                damage += 1;
                DamageUpBy = 1;
                count1Damage++;
            } else if (level >= 10)
            {
                damage += 2;
                DamageUpBy = 2;
                count2Damage++;
            }             
            maxHealth += 2;
            healthUpBy = 2;
            maxMana += 2;
            ManaUpBy = 2;
            health = maxHealth;

            DisplayLevelUpPanel();
        }
    }

    public void SetXPToNextLevel(int nextLevel)
    {
        xpToNextLevel = (int)(Mathf.Pow(2, nextLevel) + 8);
    }

    public void DisplayLevelUpPanel()
    {
        Time.timeScale = .3f;

        if (level % 5 == 0)
        {
            ManaRegenUI.SetActive(true);
            ManaRegenInfo.text = "Mana Regen +" + ManaRegenUpBy;
        }
        else
        {
            ManaRegenUI.SetActive(false);
        }
        DamageInfo.text = "Damage +" + DamageUpBy;
        healthInfo.text = "Health +" + healthUpBy;
        ManaInfo.text = "Mana +" + ManaUpBy;

        GameUI.SetActive(false);
        //GM.DisableGUI();
        Time.timeScale = 0f;
        levelUpUI.SetActive(true);
        levelupuiActive = true;
        //StartCoroutine(WaitBeforeCanClick());
    }

    public void DisableLevelUpPanel()
    {
        levelUpUI.SetActive(false);
        levelupuiActive = false;
        GameUI.SetActive(true);
        //GM.EnableGUI();
        /*if (JM.journalUI.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else if (!JM.journalUI.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 1f;
        }*/
        Time.timeScale = 1f;
    }

    IEnumerator WaitBeforeCanClick()
    {
        yield return new WaitForSeconds(2);
        GameUI.SetActive(false);
        Time.timeScale = 0f;
        levelUpUI.SetActive(true);
        levelupuiActive = true;
    }
   
    /// <summary>
    /// GET FUNCTIONS
    /// </summary>

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetMana()
    {
        return mana;
    }

    public float GetMaxMana()
    {
        return maxMana;
    }
}
