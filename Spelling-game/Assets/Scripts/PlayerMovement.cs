using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private float moveHorizontal;
    private bool jump = false;
    private bool crouch = false;

    public CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            Debug.Log("crouching");
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            Debug.Log("not crouching");
        }
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(moveHorizontal * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}


