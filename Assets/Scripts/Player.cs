using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _move_speed = 10f;
    [SerializeField] private float _fly_speed = 10f;

    private GameObject _player;
    private PlayerInput _input;
    private CharacterController _chara;
    private GameObject _cam;

    private Vector2 _move_direction;
    private float _fly_up = 0;
    private float _fly_down = 0;

    private void Awake()
    {
      Debug.Log("Player Awake");
      TryGetComponent(out _input);
      TryGetComponent(out _chara);
      _cam = GameObject.Find("Main Camera");
    }

    private void OnEnable()
    {
        Debug.Log("Player OnEnable");
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMove;
        _input.actions["FlyUp"].performed += OnFlyUp;
        _input.actions["FlyUp"].canceled += OnFlyUpStop;
        _input.actions["FlyDown"].performed += OnFlyDown;
        _input.actions["FlyDown"].canceled += OnFlyDownStop;
    }

    private void OnDisable()
    {
        Debug.Log("Player OnDisable");
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMove;
        _input.actions["FlyUp"].performed -= OnFlyUp;
        _input.actions["FlyUp"].canceled -= OnFlyUpStop;
        _input.actions["FlyDown"].performed -= OnFlyDown;
        _input.actions["FlyDown"].canceled -= OnFlyDownStop;
    }

    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
      Debug.Log("Player OnMove");
      _move_direction = obj.ReadValue<Vector2>();
      Debug.Log("_move_direction: " + _move_direction);
    }

    private void OnFlyUp(InputAction.CallbackContext obj)
    {
      Debug.Log("Player OnFlyUp");
      _fly_up = 1f;
    }

    private void OnFlyUpStop(InputAction.CallbackContext obj)
    {
      Debug.Log("Player OnFlyUpStop");
      _fly_up = 0f;
    }

    private void OnFlyDown(InputAction.CallbackContext obj)
    {
      Debug.Log("Player OnFlyDown");
      _fly_down = -1f;
    }

    private void OnFlyDownStop(InputAction.CallbackContext obj)
    {
      Debug.Log("Player OnFlyDownStop");
      _fly_down = 0f;
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_move_direction.x, 0, _move_direction.y).normalized;
        Vector3 _move = direction * _move_speed * Time.deltaTime;
        float fly_direction = _fly_up + _fly_down;
        _move += new Vector3(0f, fly_direction, 0f) * _fly_speed * Time.deltaTime;
        _chara.Move(_move);
    }

    private void Update()
    {
      Move();
    }
}
