using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollect : MonoBehaviour
{
    public int objectType; // 1 - coin, 2 - red coin, 3 - key

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (objectType)
            {
                case 1:
                    PlayerStats.instance.coinAmount++;
                    break;
                case 2:
                    PlayerStats.instance.coinAmount += 5;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
