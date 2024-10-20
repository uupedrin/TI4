using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region References/Variables
    [SerializeField] LayerMask mask;
    float currentMaxSpeed;
    float moveForce;

    [Header("Ground Movement Settings")]
    [SerializeField] [Tooltip ("Move speed while grounded")] float groundMoveForce;
    //[SerializeField] [Tooltip ("Drag while grounded")] float groundDrag;
    //[SerializeField] [Tooltip ("Maximum possible move speed while grounded")] float groundMaxSpeed;
    
    [Header("Air Movement Settings")]
    [SerializeField] [Tooltip ("Move speed while not grounded")] float airMoveForce;
    //[SerializeField] [Tooltip ("Drag while not grounded")] float airDrag;
    //[SerializeField] [Tooltip ("Maximum possible move speed while not grounded")] float airMaxSpeed;
    //[SerializeField] [Tooltip ("Maximum possible move speed while falling")] float maxFallSpeed;
    [SerializeField] [Tooltip ("Force applied downward while not moving up")] float downForce;
    
    [Header("Jump Settings")]
    [SerializeField] [Tooltip ("How strong the Jump's impulse is")] float jumpForce;
    [SerializeField] [Tooltip ("How long you can hold down the Jump button for")] float holdJumpTime;
    [SerializeField] [Tooltip ("How much stronger the Jump gets when holding the button down")] float holdJumpForce;


    [Header("Booleans")]
    [SerializeField] [Tooltip ("Whether or not the Double Jump power up is active")] bool powerUpDoubleJump;
    [SerializeField] [Tooltip ("Whether or not the Dash power up is active")] bool powerUpDash; 

    [Header("Dash Settings")]
    
    [SerializeField] [Tooltip ("How strong the Dash's impulse is")] float dashForce;
    [SerializeField] [Tooltip ("How long you can hold down the Dash button for")] float dashDuration;
    [SerializeField] [Tooltip ("How much stronger the Dash gets when holding the button down")] float holdDashForce;
    [SerializeField] [Tooltip ("How long you have to wait to Dash again, after the Dash is over")] float dashCooldown;
    float lastDash;

    [Header("References")]

    [SerializeField] [Tooltip ("What the player is looking at")] GameObject guide;
    
    Vector3 orientation;
    bool isGrounded = false, isJumping = false, canDoubleJump = false, isDashing = false;
    InputReader _reader;
    Rigidbody _body;
    Vector2 inputDirection;
    #endregion
    
    #region SetUp
    void OnEnable()
    {
        _reader.MovementEvent += SetInputDirection;
        _reader.JumpPressedEvent += Jump;
        _reader.JumpReleasedEvent += InterruptJump;
        _reader.DashPressedEvent += Dash;
        _reader.DashReleasedEvent += InterruptDash;
        lastDash = Time.time;
    }
    void OnDisable()
    {
        _reader.MovementEvent -= SetInputDirection;
        _reader.JumpPressedEvent -= Jump;
        _reader.JumpReleasedEvent -= InterruptJump;
        _reader.DashPressedEvent -= Dash;
        _reader.DashReleasedEvent -= InterruptDash;
    }

    void SetInputDirection(Vector2 value)
    {
        inputDirection = value;
        orientation = guide.transform.forward * inputDirection.y + guide.transform.right * inputDirection.x;
        transform.forward = new Vector3(orientation.x, 0, orientation.z);
    }

    void Awake()
    {
        _reader = InputReader.GetInputReader();
        _reader.SetGameplay();
        _body = GetComponent<Rigidbody>();
        
        moveForce = airMoveForce;
    }
    #endregion

    void FixedUpdate()
    {
        IsGroundedCheck();
        Move();
    }

    public void IsGroundedCheck()
    {
        isGrounded = Physics.BoxCast(transform.position, new Vector3(1, .1f, 1), Vector3.down, transform.rotation, 1, mask);
        //WHILE ON THE GROUND: use groundMoveForce, use groundDrag
        if(!isDashing)
        {
            if(isGrounded) 
            {
                moveForce = groundMoveForce;
                //_body.drag = groundDrag;
                //currentMaxSpeed = groundMaxSpeed;
                if(powerUpDoubleJump) canDoubleJump = true;
            }
            //WHILE IN THE AIR: use airMoveForce, use airDrag, PushDown can be applied
            else 
            {
                moveForce = airMoveForce;
                //_body.drag = airDrag;
                //currentMaxSpeed = airMaxSpeed;
                PushDown();
                if(!powerUpDoubleJump) canDoubleJump = false;
            }
        }
        //_body.velocity = new Vector3(Math.Clamp(_body.velocity.x, -currentMaxSpeed, currentMaxSpeed), Math.Clamp(_body.velocity.y, -maxFallSpeed, maxFallSpeed), Math.Clamp(_body.velocity.z, -currentMaxSpeed, currentMaxSpeed));
        Debug.Log(isGrounded);
    }

    void Move()
    {
        if(!isDashing) _body.AddForce(orientation * moveForce);
        else _body.AddForce(_body.velocity * holdDashForce);
        if(isJumping) _body.AddForce(Vector3.up * holdJumpForce);
    }

    void Jump()
    {
        if(isGrounded || canDoubleJump)
        {
            //Freezes position in Y, then immediately unfreezes. This causes the velocity to reset.
            _body.constraints = RigidbodyConstraints.FreezePositionY;
            _body.constraints = RigidbodyConstraints.FreezeRotation;
            _body.AddForce(Vector3.up * jumpForce);
            isJumping = true;
            Invoke("InterruptJump", holdJumpTime);
            if(!isGrounded) canDoubleJump = false;
        }
    }

    void InterruptJump()
    {
        isJumping = false;
    }
    
    void PushDown()
    {
        if(_body.velocity.y < 0) _body.AddForce(Vector3.down * downForce);
    }

    void Dash()
    {
        if(powerUpDash && Time.time >= lastDash + dashCooldown)
        {
            //Freeze ALL. This causes all velocity to reset, so the previous momentum does not affect the dashes' directions.
            _body.constraints = RigidbodyConstraints.FreezeAll;
            _body.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            _body.AddForce(new Vector3(inputDirection.x, 0, inputDirection.y) * dashForce);
            isDashing = true;
            Invoke("InterruptDash", dashDuration);
        }
    }

    void InterruptDash()
    {
        isDashing = false;
        _body.constraints = RigidbodyConstraints.FreezeRotation;
        lastDash = Time.time;
    }
}