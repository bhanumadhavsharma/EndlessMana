using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D player_rb;
    public Camera cam;
    public GameObject crosshairs;
    //public Animator anim;
    
    private Vector2 movement;
    //private Vector2 mousePos; //OLD CODE
    private Vector3 mousePos; //NEW CODE


    private void Awake()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        } ///////////////////////////////////////////
        Cursor.visible = false; /////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); ////////

        //new code
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        TurnToFaceMouse();
        moveSpeed = PlayerStats.instance.moveSpeed;
    }

    private void FixedUpdate()
    {
        player_rb.MovePosition(player_rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        /*Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle; */ //OLD CODE
    }

    void TurnToFaceMouse()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        crosshairs.transform.position = new Vector2(mousePos.x, mousePos.y);
        crosshairs.transform.rotation = Quaternion.identity;
        //Debug.Log(transform.rotation);
    }
}
