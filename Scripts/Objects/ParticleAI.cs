using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAI : MonoBehaviour
{
    bool readyToRemove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (readyToRemove)
        {
            Destroy(gameObject, 1);
        }
    }

    private void OnBecameVisible()
    {
        readyToRemove = true;
    }
}
