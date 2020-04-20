using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask
        whatIsRoom;
    public LevelGeneration
        levelGen;

    public bool simpleMaps;

    // Update is called once per frame
    void Update()
    {
        simpleMaps = PlayerStats.instance.simpleMapsActive;

        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection == null && levelGen.stopGeneration == true)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            if (simpleMaps)
            {
                Instantiate(levelGen.closedRoom, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
