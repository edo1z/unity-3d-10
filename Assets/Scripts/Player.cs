using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _move_speed = 10f;
    [SerializeField] private float _fly_speed = 10f;
    [SerializeField] private float _look_sensitive_x = 0.4f;
    [SerializeField] private float _look_sensitive_y = 0.1f;

    private PlayerInputHandler _input;
    private GameObject _player;
    private CharacterController _chara;
    private GameObject _cam;

    private void Awake()
    {
        TryGetComponent(out _chara);
        TryGetComponent(out _input);
        _cam = GameObject.Find("Main Camera");
    }

    private void Aim()
    {
      Vector2 angles = _cam.transform.eulerAngles;
      Vector2 direction = _input.GetLookDirection();
      float x = angles.x + direction.y * 0.01f * _look_sensitive_y * -1f;
      float y = angles.y + direction.x * 0.01f * _look_sensitive_x;
      _cam.transform.rotation = Quaternion.Euler(x, y, 0);
    }

    private void Move()
    {
        Vector3 move = _input.GetMoveDirection() * _move_speed * Time.deltaTime;
        move += new Vector3(0f, _input.GetFlyDirection(), 0f) * _fly_speed * Time.deltaTime;
        _chara.Move(move);
    }

    private void Update()
    {
        Aim();
        Move();
    }
}
