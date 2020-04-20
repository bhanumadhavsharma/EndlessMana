using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateRoom : MonoBehaviour
{
    public GameObject[]
        enemies1to4;
    public GameObject[]
        enemies5to9;
    public GameObject[]
        enemies10to14;
    public GameObject[]
        enemies15to19;
    public GameObject[]
        collectibles;
    public LayerMask
        whatIsPlayer;
    public bool
        roomPopulated;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemies1to4.Length; i++)
        {
            enemies1to4[i].GetComponent<SpawnObject>().enabled = false;
        }
        for (int i = 0; i < enemies5to9.Length; i++)
        {
            enemies5to9[i].GetComponent<SpawnObject>().enabled = false;
        }
        for (int i = 0; i < enemies10to14.Length; i++)
        {
            enemies10to14[i].GetComponent<SpawnObject>().enabled = false;
        }
        for (int i = 0; i < enemies15to19.Length; i++)
        {
            enemies15to19[i].GetComponent<SpawnObject>().enabled = false;
        }
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].GetComponent<SpawnObject>().enabled = false;
        }
        roomPopulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && roomPopulated == false)
        {
            if (PlayerStats.instance.level < 5)
            {
                for (int i = 0; i < enemies1to4.Length; i++)
                {
                    enemies1to4[i].SetActive(true);
                    enemies1to4[i].GetComponent<SpawnObject>().enabled = true;
                }
            }
            if (PlayerStats.instance.level >= 5 && PlayerStats.instance.level < 10)
            {
                for (int i = 0; i < enemies5to9.Length; i++)
                {
                    enemies1to4[i].SetActive(true);
                    enemies1to4[i].GetComponent<SpawnObject>().enabled = true;
                    enemies5to9[i].SetActive(true);
                    enemies5to9[i].GetComponent<SpawnObject>().enabled = true;
                }
            }
            if (PlayerStats.instance.level >= 10 && PlayerStats.instance.level < 15)
            {
                for (int i = 0; i < enemies10to14.Length; i++)
                {
                    enemies1to4[i].SetActive(true);
                    enemies1to4[i].GetComponent<SpawnObject>().enabled = true;
                    enemies5to9[i].SetActive(true);
                    enemies5to9[i].GetComponent<SpawnObject>().enabled = true;
                    enemies10to14[i].SetActive(true);
                    enemies10to14[i].GetComponent<SpawnObject>().enabled = true;
                }
            }
            if (PlayerStats.instance.level >= 15)
            {
                for (int i = 0; i < enemies15to19.Length; i++)
                {
                    enemies1to4[i].SetActive(true);
                    enemies1to4[i].GetComponent<SpawnObject>().enabled = true;
                    enemies5to9[i].SetActive(true);
                    enemies5to9[i].GetComponent<SpawnObject>().enabled = true;
                    enemies10to14[i].SetActive(true);
                    enemies10to14[i].GetComponent<SpawnObject>().enabled = true;
                    enemies15to19[i].SetActive(true);
                    enemies15to19[i].GetComponent<SpawnObject>().enabled = true;
                }
            }

            for (int i = 0; i < collectibles.Length; i++)
            {
                collectibles[i].SetActive(true);
                collectibles[i].GetComponent<SpawnObject>().enabled = true;
            }

            roomPopulated = true;
        }
    }
}
