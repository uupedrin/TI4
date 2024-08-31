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
    [SerializeField] float groundMoveForce;
    [SerializeField] float groundDrag, groundMaxSpeed;
    
    [Header("Air Movement Settings")]
    [SerializeField] float airMoveForce;
    [SerializeField] float airDrag, airMaxSpeed, maxFallSpeed, downForce;
    
    [Header("Jump Settings")]
    [SerializeField] float jumpForce;
    [SerializeField] float holdJumpForce, holdJumpTime;

    [Header("Booleans")]
    [SerializeField] bool powerUpDoubleJump;
    [SerializeField] bool powerUpDash; 

    [Header("Dash Settings")]
    
    [SerializeField] float dashForce;
    [SerializeField] float holdDashForce, dashDuration, dashCooldown;
    float lastDash;
    
    
    bool isGrounded = false, canJump = false, isJumping = false, canDoubleJump = false, isDashing = false;
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
        //WHILE ON THE GROUND: use groundMoveForce, use groundDrag, canJump is true
        if(!isDashing)
        {
            if(isGrounded) 
            {
                canJump = true;
                moveForce = groundMoveForce;
                _body.drag = groundDrag;
                currentMaxSpeed = groundMaxSpeed;
                if(powerUpDoubleJump) canDoubleJump = true;
            }
            //WHILE IN THE AIR: use airMoveForce, use airDrag, canJump can become false, PushDown can be applied
            else 
            {
                canJump = false;
                moveForce = airMoveForce;
                _body.drag = airDrag;
                currentMaxSpeed = airMaxSpeed;
                PushDown();
                if(!powerUpDoubleJump) canDoubleJump = false;
            }
        }
        _body.velocity = new Vector3(Math.Clamp(_body.velocity.x, -currentMaxSpeed, currentMaxSpeed), Math.Clamp(_body.velocity.y, -maxFallSpeed, maxFallSpeed), Math.Clamp(_body.velocity.z, -currentMaxSpeed, currentMaxSpeed));
        Debug.Log(isGrounded);
    }

    void Move()
    {
        if(!isDashing) _body.AddForce(new Vector3(inputDirection.x, 0, inputDirection.y) * moveForce);
        else _body.AddForce(_body.velocity * holdDashForce);
        if(isJumping) _body.AddForce(Vector3.up * holdJumpForce);
    }

    void Jump()
    {
        if(canJump || canDoubleJump)
        {
            _body.constraints = RigidbodyConstraints.FreezePositionY;
            _body.constraints = RigidbodyConstraints.FreezeRotation;
            _body.AddForce(Vector3.up * jumpForce);
            isJumping = true;
            Invoke("InterruptJump", holdJumpTime);
            if(!canJump) canDoubleJump = false;
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
            _body.constraints = RigidbodyConstraints.FreezeAll;
            _body.constraints = RigidbodyConstraints.FreezePositionY;
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