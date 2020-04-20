using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkIfToggleEnabled : MonoBehaviour
{
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOptions.instance.simpleMapsActive)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }
}
