using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _move_speed = 10f;
    [SerializeField] private float _fly_speed = 10f;

    private PlayerInputHandler _input;
    private GameObject _player;
    private CharacterController _chara;

    private void Awake()
    {
        TryGetComponent(out _chara);
        TryGetComponent(out _input);
    }

    private void Move()
    {
        Vector3 move = _input.GetMoveDirection() * _move_speed * Time.deltaTime;
        move += new Vector3(0f, _input.GetFlyDirection(), 0f) * _fly_speed * Time.deltaTime;
        _chara.Move(move);
    }

    private void Update()
    {
        Move();
    }
}
