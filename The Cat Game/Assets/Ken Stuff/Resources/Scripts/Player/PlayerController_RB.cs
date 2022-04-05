﻿/*********************************************************************************************************
 * Class players movement
 * -  Add this component to the players root object
 * *******************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_RB : MonoBehaviour {

    // inspector variables
    [SerializeField, Tooltip("Player movement speed")]
    private float speed = 10.0f;
    [SerializeField, Tooltip("Player movement speed between worlds")]
    private float transferSpeed = 10.0f;
    [SerializeField, Tooltip("Player jump force")]
    private float jumpForce = 10.0f;
    [SerializeField, Tooltip("Player landing distance")]
    private float maxJumpHeight = 10.0f;
    [SerializeField, Tooltip("Landing Particles")]
    private ParticleSystem landingParticles;
    [SerializeField, Tooltip("Distance from world to play landing particles (percentage of distance between worlds)"), Range(0.0f, 1.0f)]
    private float landDistance = 0.4f;
    [SerializeField, Tooltip("TakeOff Particles")]
    private ParticleSystem takeOffParticles;

    // privates
    private List<GameObject> _worlds = new List<GameObject>();
    private int _currentWorld = 0;
    private int _prevWorld = 0;
    private Vector3 _moveDirection;
    private Rigidbody _playerRB;
    private Transform _playerMesh;
    private FakeGravityBody _worldGravity;
 
    // transfer
    private bool _transfering = false;
    private bool _landed = false;
    private float _worldDistance = 0;

    // Use this for initialization
    private void Start()
    {
        // set player details
        _playerRB = GetComponent<Rigidbody>();
        _playerMesh = transform.GetChild(0).transform;
        _worldGravity = GetComponent<FakeGravityBody>();
        // find worlds in scene
        _worlds.AddRange(GameObject.FindGameObjectsWithTag("World"));
        _currentWorld = CurrentWorldIndex();
        // update player speed
        speed = SpeedUpdate();
    }

    // Update is called once per frame
    private void Update()
    {
        // if changing worlds
        if (_transfering)
        {
            return;
        }

        // update move direction
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // world transfer
        if (Input.GetKeyDown("e"))
        {
            WorldTransfer();
        }

        // jump
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }

        // rotate player to face the right direction
        RotateForward();
    }

    // FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        // if changing worlds
        if (_transfering)
        {
            return;
        }
        // update movement
        _playerRB.MovePosition(_playerRB.position + transform.TransformDirection(_moveDirection * speed * Time.deltaTime));
    }

    /// <summary>
    /// Player Jump
    /// </summary>
    private void Jump()
    {
        // get current jump height
        float jumpHeight = Vector3.Distance(_worlds[_currentWorld].transform.position, transform.position) - maxJumpHeight;
        // limit height to which jump is applied
        if (jumpHeight < maxJumpHeight)
        {
            // get direction of gravity
            Vector3 gravityDir = (_worlds[_currentWorld].transform.position - transform.position).normalized;
            // apply force against gravity
            _playerRB.AddForce(-gravityDir * jumpForce, ForceMode.Impulse);
            // play take off particle effect
            takeOffParticles.Play();
        }
    }

    /// <summary>
    /// Initialise player transfer between worlds
    /// </summary>
    private void WorldTransfer()
    {
        // launch player
        Jump();
        // disconnect gravity
        _worldGravity.Attractor = null;
        // increment world ID
        if (_currentWorld + 1 >= _worlds.Count)
        {
            _prevWorld = _currentWorld;
            _currentWorld = 0;
        }
        else
        {
            _prevWorld = _currentWorld;
            _currentWorld++;
        }
        // initialise planet transfer
        _transfering = true;
        // distance between _worlds
        _worldDistance = Vector3.Distance(_worlds[_prevWorld].transform.position, _worlds[_currentWorld].transform.position) -
                                        (_worlds[_prevWorld].GetComponent<FakeGravity>().WorldSize + _worlds[_currentWorld].GetComponent<FakeGravity>().WorldSize);
        // start change worlds coroutine
        StartCoroutine("ChangeWorlds");
    }
    
    /// <summary>
    /// Corountine that controls changing worlds
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeWorlds()
    {
        while (_transfering)
        {
            // move to travel height
            yield return StartCoroutine("TakeOff");
            // rotate to target
            yield return StartCoroutine("RotateToTarget");
            // travel to target
            yield return StartCoroutine("TravelToTarget");
        }
    }

    /// <summary>
    /// Player take off coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeOff()
    {
        bool done = false;

        while (!done)
        {
            float jumpDistance = Vector3.Distance(_worlds[_prevWorld].transform.position, transform.position);
            // transfer once player reaches max jump height
            if (jumpDistance > (maxJumpHeight * 2.5))
            {
                // reduce velocity
                if (_playerRB.velocity.magnitude > 2f)
                {
                    _playerRB.velocity -= (transform.up * 10) * Time.deltaTime; ;
                }
                else
                {
                    _playerRB.velocity = Vector3.zero;
                    // finish coroutine
                    done = true;
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine to rotate player to target world
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateToTarget()
    {
        bool done = false;

        while(!done)
        {
            // set move direction
            _moveDirection = (_worlds[_currentWorld].transform.position - transform.position).normalized;
            // rotate player
            transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.FromToRotation(Vector3.up, _moveDirection),
                        Time.deltaTime
                        );
            // check if rotation is complete
            if (Vector3.Distance(_moveDirection, transform.up) <= 0.01f)
            {
                // play take off particle effect
                takeOffParticles.Play();
                // finish coroutine
                done = true;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine to move player to new world and land
    /// </summary>
    /// <returns></returns>
    private IEnumerator TravelToTarget()
    {
        bool done = false;

        while(!done)
        {
            // get direction to world and move player
            _moveDirection = (_worlds[_currentWorld].transform.position - transform.position).normalized;
            // get distance from new world
            float distance = Vector3.Distance(_worlds[_currentWorld].transform.position, transform.position);
            // start landing rotation before hitting atmosphere
            if (distance < (_worldDistance * landDistance) + 5)
            {
                // rotate to land
                transform.rotation = Quaternion.Slerp(
                                                    transform.rotation,
                                                    Quaternion.FromToRotation(-Vector3.up, _moveDirection),
                                                    Time.deltaTime
                                                    );
                // move slower now closer to world
                _playerRB.MovePosition(_playerRB.position + (_moveDirection * (transferSpeed * 0.5f) * Time.deltaTime));
            }
            else
            {
                // apply normal travel speed
                _playerRB.MovePosition(_playerRB.position + (_moveDirection * transferSpeed * Time.deltaTime));
            }
            // check if entering atmosphere
            if (distance < (_worldDistance * landDistance))
            {
                // if landed then arrived at new world
                if (_landed)
                {
                    // set new attractor
                    _worldGravity.Attractor = _worlds[_currentWorld].GetComponent<FakeGravity>();
                    // reset transfer state
                    ResetState();
                    // finish coroutine
                    done = true;
                }
                else
                {
                    // play landing particles
                    landingParticles.Play();
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// Rotate player to face direction of movement
    /// </summary>
    private void RotateForward()
    {
        Vector3 dir = _moveDirection;
        // calculate angle and rotation
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.up);
        // only update rotation if direction greater than zero
        if (Vector3.Magnitude(dir) > 0.0f)
        {
            _playerMesh.localRotation = targetRotation;
        }
    }

    /// <summary>
    /// Update player speed based on world
    /// </summary>
    private float SpeedUpdate()
    {
        float newSpeed = speed;
        // update speed value
        if (_worldGravity.Attractor.gameObject.name == "PlaneWorld")
        {
            newSpeed = speed / 2;
        }
        // return result
        return newSpeed;
    }

    /// <summary>
    /// Get current world player is on
    /// </summary>
    private int CurrentWorldIndex()
    {
        int worldIndex = 0;
        // get name of current world player is attracted to
        string worldName = _worldGravity.Attractor.gameObject.name;
        // iterate through list of worlds
        int count = _worlds.Count;
        for (int i = 0; i < count; i++)
        {
            // check if world in list has same name as curretn attractor
            if (worldName == _worlds[i].name)
            {
                worldIndex = i;
            }
        }
        // return result
        return worldIndex;
    }

    /// <summary>
    /// Reset bools used for world transfer
    /// </summary>
    private void ResetState()
    {
        _transfering = false;
        _landed = false;
    }

    /// <summary>
    /// Called when player enters a collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // if player transfering between worlds and has collided with a world
        if(_transfering && collision.transform.tag == "World")
        {
            // player landed on world
            _landed = true;
            landingParticles.Stop();
        }
    }
}
