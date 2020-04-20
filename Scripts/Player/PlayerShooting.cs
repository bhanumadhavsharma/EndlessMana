using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    // NOT DOING SPELL 2 ANYMORE

    public Transform
        firePoint;
    public GameObject
        bulletPrefab,
        firePointObject;
    //        bullet2Prefab;
    public float
        bulletForce = 20f;
    //        bullet2Force = 10f;
    public ParticleSystem
        spellParticles;
    //        spell2Particles;

    public int
        spellEffect,
        manaCost;
    public Color
        spellColor,
        effectColor;
    float
        m_Red,
        m_Blue,
        m_Green;

    public int
        selectedSpell = 1,
        totalSpells = 2,
        standardSpellCost = 2,
        iceSpellCost = 3,
        fireSpellCost = 5,
        forceSpellCost = 2;

    public bool
        spellCostChanged = false;

    private void Start()
    {
        if (totalSpells == 1)
        {
            selectedSpell = PlayerStats.instance.selectedSpell;
            totalSpells = PlayerStats.instance.totalSpells;
        }
        if (spellCostChanged)
        {
            standardSpellCost = PlayerStats.instance.standardSpellCost;
            iceSpellCost = PlayerStats.instance.iceSpellCost;
            fireSpellCost = PlayerStats.instance.fireSpellCost;
            forceSpellCost = PlayerStats.instance.forceSpellCost;
            spellCostChanged = false;
        }
        PlayerStats.instance.standardSpellCost = standardSpellCost;
        PlayerStats.instance.iceSpellCost = iceSpellCost;
        PlayerStats.instance.fireSpellCost = fireSpellCost;
        PlayerStats.instance.forceSpellCost = forceSpellCost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && PlayerStats.instance.mana > manaCost)
        {
            Shoot();
            PlayerStats.instance.UseSpell(manaCost);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedSpell >= (totalSpells))
            {
                selectedSpell = 1;
                SetSpell(selectedSpell);
            }
            else
            {
                selectedSpell++;
                SetSpell(selectedSpell);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedSpell <= 1)
            {
                selectedSpell = totalSpells;
                SetSpell(selectedSpell);
            }
            else
            {
                selectedSpell--;
                SetSpell(selectedSpell);
            }
        }
        PlayerStats.instance.selectedSpell = selectedSpell;
        PlayerStats.instance.totalSpells = totalSpells;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        var main = spellParticles.main;

        bulletSprite.color = spellColor;
        bulletScript.effect = spellEffect;
        main.startColor = effectColor;
        Instantiate(spellParticles, firePoint.position, firePoint.rotation);
        bulletRB.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void SetSpell(int spellNumber)
    {
        switch (spellNumber)
        {
            case 1: //standard
                spellColor = new Color(0.58f, .56f, .56f);
                effectColor = new Color(.43f, .43f, .43f);
                manaCost = standardSpellCost;
                spellEffect = 1;
                break;
            case 2: //ice
                spellColor = new Color(0.11f, .89f, .84f);
                effectColor = new Color(.07f, .58f, .54f);
                manaCost = iceSpellCost;
                spellEffect = 2;
                break;
        }

        SpriteRenderer firePointSprite = firePointObject.GetComponent<SpriteRenderer>();
        firePointSprite.color = spellColor;
    }
}
