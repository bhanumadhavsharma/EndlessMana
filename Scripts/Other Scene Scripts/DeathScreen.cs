using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI
        deathTextUI;
    string deathText;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        DisplayDeathText();
        if (GameOptions.instance.simpleMapsActive)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        Cursor.visible = true;
    }

    void DisplayDeathText()
    {
        deathText = "You died. \nAny unspent coins are lost. \nAny items bought in shop will carry over to the next life."; //\nYou earned " + PlayerStats.instance.coinAmount + " coins.";
        StartCoroutine(EffectTypewriter(deathText, deathTextUI));
    }

    private IEnumerator EffectTypewriter(string text, TextMeshProUGUI dialog)
    {
        foreach (char character in text.ToCharArray())
        {
            dialog.text += character;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void IfToggleChanged(bool value)
    {
        GameOptions.instance.simpleMapsActive = value;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}
