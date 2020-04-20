using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuObject;
    public GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOpenOptionsButton()
    {
        MainMenuObject.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnCloseOptionsButton()
    {
        optionsMenu.SetActive(false);
        MainMenuObject.SetActive(true);
    }

    public void SimpleMapsChecked(bool value)
    {
        GameOptions.instance.simpleMapsActive = value;
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}
