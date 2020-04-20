using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelGeneration LG;
    private PlayerStats PS;
    private GameUIManager GUI;

    // Start is called before the first frame update
    void Start()
    {
        LG = GameObject.Find("LevelGeneration").GetComponent<LevelGeneration>();
        GUI = GetComponent<GameUIManager>();
        GUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LG.stopGeneration)
        {
            GUI.enabled = true;
        }
    }

    public void DisableGUI()
    {
        GUI.enabled = false;
    }

    public void EnableGUI()
    {
        GUI.enabled = true;
    }
}
