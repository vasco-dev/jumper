using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //angle of the vector direction of the player jump, IN PERCENT
    [SerializeField]
    private float _jumpAngle = 65f;

    //ammount to add to jump force, PER FRAME
    [SerializeField]
    private float _jumpForceIncrement = 100f;

    //max force for the player jump
    [SerializeField]
    private float _maxJumpForce = 300f;

    //current force for the current jump
    private float _currentJumpForce = 0f;


    //is the player touching and holding the screen
    private bool _isHolding = false;
    //is the player holding the right side
    private bool _holdingRight = false;

    //screen size of the screen, IN PIXELS
    private float _screenSize = 1080f;

    //Player input actions
    private PlayerIA _playerInputs;
    private InputAction _positionAction;
    private InputAction _pressedAction;

    // rigid body
    public Rigidbody Body { get; private set; }

    // singleton instance
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }

        //get the screen width in pixels
        _screenSize = Screen.width;

        //create and enable the player input
        _playerInputs = new PlayerIA();
        _playerInputs.Enable();
        _pressedAction = _playerInputs.Player.Pressed;
        _positionAction = _playerInputs.Player.Position;
        //set events for touching and releasing the screen
        _pressedAction.started += InputOnTap;
        _pressedAction.canceled += InputOnRelease;

        //get player's rigidbody
        Body = GetComponent<Rigidbody>();

    }

    /// <summary>
    ///  called by pressed input event
    /// </summary>
    private void InputOnTap(InputAction.CallbackContext obj)
    {
        // detect which side of the screen the touch was
        float inputX = _positionAction.ReadValue<float>();
        inputX /= _screenSize;

        if (inputX >= 0.5f)
        {
            //Right
            _holdingRight = true;
        }
        else
        {            
            //Left
            _holdingRight = false;
        }

        // start holding and coroutine
        _isHolding = true;

        StartCoroutine(IHoldPress());
    }

    /// <summary>
    ///  called by released input event
    /// </summary>
    private void InputOnRelease(InputAction.CallbackContext obj)
    {
        //stop holding
        _isHolding = false;

        //jump for the direction the player is holding
        JumpToDir(_holdingRight);

    }

    /// <summary>
    /// calculate and make the player jump
    /// </summary>
    /// <param name="isRight"> true = right | false = left </param>
    private void JumpToDir(bool isRight)
    {
        Vector3 dir;

        // set the direction to the side the player is holding
        if (isRight){
            dir = Vector3.right;
        }
        else{
            dir = Vector3.left;
        }

        // convert the direction to have the correct angle, IN PERCENT, and normalize it after
        dir *= (100f - _jumpAngle);
        dir += Vector3.up *_jumpAngle;
        dir.Normalize();

        // reset player velocity
        Body.velocity = Vector3.zero;

        // make player jump with the direction and force
        Body.AddForce(dir * _currentJumpForce);

        // reset current jump force
        _currentJumpForce = 0;

    }

    /// <summary>
    ///  run every fixed update while the player is holding the finger on the screen
    ///  increments the force of the jump until max
    /// </summary>
    private IEnumerator IHoldPress()
    {
        //keep repeating every fixed frame until player stops holding
        while(_isHolding)
        {
            // if not at the max, increment the force
            if (_currentJumpForce < _maxJumpForce){
                _currentJumpForce += _jumpForceIncrement;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}