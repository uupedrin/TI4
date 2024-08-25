using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region References/Variables
    [SerializeField] float groundDrag;
    [SerializeField] float airDrag;
    [SerializeField] LayerMask mask;
    public float moveForce;
    [SerializeField] float groundMoveForce;
    [SerializeField] float airMoveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float holdJumpForce;
    [SerializeField] float holdJumpTime;
    bool canJump = false;
    bool isJumping = false;
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
        Move();
    }

    public bool IsGroundedCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.01f, mask);
    }
    
    void Jump()
    {
        if(canJump)
        {
            _body.AddForce(Vector3.up * jumpForce);
            isJumping = true;
            Invoke("InterruptJump", holdJumpTime);
            //if(jumpPowerUp && !IsGroundedCheck) canJump = false;
        }
    }

    void InterruptJump()
    {
        isJumping = false;
    }
    
    void Move()
    {
        _body.AddForce(new Vector3(inputDirection.x, 0, inputDirection.y) * moveForce);
        if(isJumping) _body.AddForce(Vector3.up * holdJumpForce);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (IsGroundedCheck()) 
        {
            _body.drag = groundDrag;
            moveForce = groundMoveForce;
            canJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(!IsGroundedCheck())
        {
            _body.drag = airDrag;
            moveForce = airMoveForce;
            /*if(!jumpPowerUp) */canJump = false;
        }
    }
}
