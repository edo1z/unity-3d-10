using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;
    private Vector2 _move_direction;
    private Vector2 _look_direction;
    private float _fly_up = 0;
    private float _fly_down = 0;

    private void Awake()
    {
        TryGetComponent(out _input);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked;
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
        _input.actions["Look"].performed += OnLook;
        _input.actions["Look"].canceled += OnLook;
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
        _input.actions["Look"].performed -= OnLook;
        _input.actions["Look"].canceled -= OnLook;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        _move_direction = obj.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        _look_direction = obj.ReadValue<Vector2>();
        Debug.Log("OnLook: " + _look_direction);
    }

    private void OnFlyUp(InputAction.CallbackContext obj)
    {
        _fly_up = 1f;
    }

    private void OnFlyUpStop(InputAction.CallbackContext obj)
    {
        _fly_up = 0f;
    }

    private void OnFlyDown(InputAction.CallbackContext obj)
    {
        _fly_down = -1f;
    }

    private void OnFlyDownStop(InputAction.CallbackContext obj)
    {
        _fly_down = 0f;
    }

    public Vector3 GetMoveDirection()
    {
        if (CanProcessInput())
        {
            return new Vector3(_move_direction.x, 0, _move_direction.y).normalized;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector2 GetLookDirection()
    {
        if (CanProcessInput())
        {
            return _look_direction;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public float GetFlyDirection()
    {
        if (CanProcessInput())
        {
            return _fly_up + _fly_down;
        }
        else
        {
            return 0f;
        }
    }

}
