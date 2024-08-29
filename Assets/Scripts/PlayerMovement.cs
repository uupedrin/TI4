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

    [Header("Movement Numbers")]
    float moveForce;
    [SerializeField] float groundMoveForce, airMoveForce, groundDrag, airDrag, groundMaxSpeed, airMaxSpeed, maxFallSpeed, jumpForce, holdJumpForce, holdJumpTime, downForce;
    float currentMaxSpeed;
    
    bool isGrounded = false, canJump = false, isJumping = false;
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
    }
    void OnDisable()
    {
        _reader.MovementEvent -= SetInputDirection;
        _reader.JumpPressedEvent -= Jump;
        _reader.JumpReleasedEvent -= InterruptJump;
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
        if(isGrounded) 
        {
            canJump = true;
            moveForce = groundMoveForce;
            _body.drag = groundDrag;
            currentMaxSpeed = groundMaxSpeed;
        }
        //WHILE IN THE AIR: use airMoveForce, use airDrag, canJump can become false, PushDown can be applied
        else 
        {
            moveForce = airMoveForce;
            _body.drag = airDrag;
            currentMaxSpeed = airMaxSpeed;
            PushDown();
        }
        _body.velocity = new Vector3(Math.Clamp(_body.velocity.x, -currentMaxSpeed, currentMaxSpeed), Math.Clamp(_body.velocity.y, -maxFallSpeed, maxFallSpeed), Math.Clamp(_body.velocity.z, -currentMaxSpeed, currentMaxSpeed));
        Debug.Log(isGrounded);
    }

    void OnDrawGizmos()
    {
        
    }

    void Move()
    {
        _body.AddForce(new Vector3(inputDirection.x, 0, inputDirection.y) * moveForce);
        if(isJumping) _body.AddForce(Vector3.up * holdJumpForce);
    }

    void Jump()
    {
        if(canJump)
        {
            _body.constraints = RigidbodyConstraints.FreezePositionY;
            _body.constraints = RigidbodyConstraints.FreezeRotation;
            _body.AddForce(Vector3.up * jumpForce);
            isJumping = true;
            Invoke("InterruptJump", holdJumpTime);
            if(/*jumpPowerUp && */!isGrounded) canJump = false;
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
}
