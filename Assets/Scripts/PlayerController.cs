using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // scale of the force of gravity pushing the player, do not change the player's mass, it needs to be always = 1
    [SerializeField]
    private float _gravityScale = -30f;

    // angle of the vector direction of the player jump, IN PERCENT
    [SerializeField]
    private float _jumpAngle = 65f;

    // ammount to add to jump force, PER FRAME
    [SerializeField]
    private float _jumpForceIncrement = 100f;

    // max force for the player jump
    [SerializeField]
    private float _maxJumpForce = 300f;

    // current force for the current jump
    private float _currentJumpForce = 0f;


    // is the player touching and holding the screen
    private bool _isHolding = false;
    // is the player holding the right side
    private bool _holdingRight = false;

    private bool _isGrounded = false;

    // screen size of the screen, IN PIXELS
    private float _screenSize = 1080f;

    // Player input actions
    private PlayerIA _playerInputs;
    private InputAction _positionAction;
    private InputAction _pressedAction;



    // constant force component, funcitons as the Player gravity
    private ConstantForce _gravityForce = null;

    // animator component
    private Animator _animator = null;

    // rigid body
    public Rigidbody Body { get; private set; }

    // singleton instance
    public static PlayerController Instance { get; private set; }


    //remove
    //private float TEMP_START_TIME = 0;


    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }

        // get player's ConstantForce component, is used as Gravity
        TryGetComponent<ConstantForce>(out _gravityForce);
        if(_gravityForce == null){
            Debug.LogError("No ConstantForce component");
        }

        // get animator component
        TryGetComponent<Animator>(out _animator);
        if(_animator == null){
            Debug.LogError("No Animator component");
        }

        // get player's rigidbody (cant be trygetcomponent)
        Body = GetComponent<Rigidbody>();
        if (Body == null){
            Debug.LogError("No RigidBody component");
        }

        // get the screen width in pixels
        _screenSize = Screen.width;

        // create and enable the player input
        _playerInputs = new PlayerIA();
        _playerInputs.Enable();
        _pressedAction = _playerInputs.Player.Pressed;
        _positionAction = _playerInputs.Player.Position;
        // set events for touching and releasing the screen
        _pressedAction.started += InputOnTap;
        _pressedAction.canceled += InputOnRelease;

        // set the component's force to the gravityscale variable
        _gravityForce.force = new(0f, _gravityScale, 0f);



    }
    private void LateUpdate()
    {
        _animator.SetBool("IsJumpStarting", _isHolding);
    }

    /// <summary>
    ///  called by pressed input event
    /// </summary>
    private void InputOnTap(InputAction.CallbackContext obj)
    {
        Collider[] overlapColliders;
        _isGrounded = false;

        overlapColliders = Physics.OverlapBox(transform.position, 0.5f*Vector3.one,Quaternion.identity ,Physics.AllLayers);

        foreach (Collider col in overlapColliders)
        {
            if (col.gameObject != gameObject)
            {
                //ser is grounded
                _isGrounded = true;
            }
        }




        if (_isGrounded)
        {


            // if playing in editor play mode, check if gravity value has been manually changed
            // and update the gravity component to match
#if UNITY_EDITOR
            _gravityForce.force = new(0f, _gravityScale, 0f);
#endif

            // detect which side of the screen the touch was
            float inputX = _positionAction.ReadValue<float>();
            inputX /= _screenSize;

            if (inputX >= 0.5f)
            {
                // Right
                _holdingRight = true;
            }
            else
            {
                // Left
                _holdingRight = false;
            }

            // start holding and coroutine
            _isHolding = true;

            StartCoroutine(IHoldPress());
        }
    }
    

    /// <summary>
    ///  called by released input event
    /// </summary>
    private void InputOnRelease(InputAction.CallbackContext obj)
    {
        //check if the player is grounded
        if (_isGrounded == true)
        {
            // stop holding
            _isHolding = false;

            // jump for the direction the player is holding
            JumpToDir(_holdingRight);

            // player is not grounded anymore
            _isGrounded = false;
        }
        //else
        //{
        //    Debug.Log("NOT GROUNDED");
        //}   
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
        // keep repeating every fixed frame until player stops holding
        while(_isHolding)
        {
            // if not at the max, increment the force
            if (_currentJumpForce < _maxJumpForce){
                _currentJumpForce += _jumpForceIncrement;
                //TEMP_START_TIME += Time.deltaTime;
            }
            else
            {
                _currentJumpForce = _maxJumpForce;

                //#if UNITY_EDITOR
                //    if (TEMP_START_TIME > 0)
                //    {
                //        //Debug.Log("Max force : " + _currentJumpForce + " : " + TEMP_START_TIME);
                //        TEMP_START_TIME = 0;
                //    }
                //#endif
            }
            yield return new WaitForFixedUpdate();
        }
    }
}