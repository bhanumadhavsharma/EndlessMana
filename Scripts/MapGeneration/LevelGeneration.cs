using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGeneration : MonoBehaviour
{
    public GameObject
        player;
    private Transform
        spawnPoint;

    public GameObject
        finishLine;

    public GameObject
        LoadScreen;
    public TextMeshProUGUI
        loadScreenText;
    public GameObject
         StatsUI;

    public Transform[]
        startingPositions;
    public GameObject[]
        safeRooms;
    public GameObject[]
        rooms; // index 0 = LR, index 1 = LRB, index 2 = LRT, index 3 = LRTB 
    public GameObject
        closedRoom;

    public int
        moveAmount;
    public float
        startTimeBetweenRoom = .25f,
        minX,
        maxX,
        minY;
    public bool
        stopGeneration;
    public LayerMask
        room;

    private int
        randStartingPosition;
    private int
        direction;
    private float
        timeBetweenRoom;
    private int
        downCounter;

    private int
        gameLevelNumber;

    // Start is called before the first frame update
    void Start()
    {
        //LoadScreen = GameObject.Find("GameManager/LoadScreen");
        loadScreenText.text = "Loading\nLevel " + GameOptions.instance.gameLevelNumber;
        LoadScreen.SetActive(true);
        //StatsUI = GameObject.Find("GameManager/GameUI");
        StatsUI.SetActive(false);

        randStartingPosition = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPosition].position;
        //spawnPoint.position = startingPositions[randStartingPosition].position;

        Instantiate(safeRooms[0], transform.position, Quaternion.identity);
        //StartCoroutine(WaitToSpawnPlayer());
        //var playerTemp = Instantiate(player, transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);

        gameLevelNumber = GameOptions.instance.gameLevelNumber + 1;
        GameOptions.instance.gameLevelNumber = gameLevelNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            timeBetweenRoom -= Time.deltaTime;
        }
    }

    /*IEnumerator WaitToSpawnPlayer()
    {

        yield return new WaitForSecondsRealtime(2);

        //Debug.Log("print from " + Time.fixedDeltaTime);
        var playerTemp = Instantiate(player, spawnPoint.position, Quaternion.identity);
    } */ //PLAYER SPAWN

    private void Move()
    {
        if (direction == 1 || direction == 2) // MOVE RIGHT 
        {
            downCounter = 0;
            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) // MOVE LEFT
        {
            downCounter = 0;
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5) // MOVE DOWN
        {
            downCounter++;
            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().Destruction();
                        Instantiate(safeRooms[1], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().Destruction();
                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 3;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                Instantiate(finishLine, transform.position, Quaternion.identity);

                //stop level generation
                stopGeneration = true;
                /*spawnPoint = startingPositions[randStartingPosition].transform;
                var playerTemp = Instantiate(player, spawnPoint);*/
                spawnPoint = startingPositions[randStartingPosition].transform;
                Instantiate(player, spawnPoint.position, Quaternion.identity);
                LoadScreen.SetActive(false);
                StatsUI.SetActive(true);

                //DISPLAY GAME LEVEL NUMBER
            }
        }
    }
}
