using System;
using UnityEngine;


public class EnemyMover
{
    private Transform _target;
    private Transform _transform;
    private Rigidbody _rigitbody;

    private float _moveSpeed;
    private float _rotateSpeed;
    private float _approachDistance;
    private bool _isMoving;

    public event Action<bool> Moved;

    public EnemyMover(Transform target, Transform transform, Rigidbody rigitbody)
    {
        _target = target;
        _transform = transform;
        _rigitbody = rigitbody;
        _moveSpeed = 5f;
        _rotateSpeed = 0.3f;
        _approachDistance = 2f;
    }

    public void Tick()
    {
        Follow();
    }

    private void Move()
    {
        Vector3 movement = Vector3.MoveTowards(_transform.position, _target.position, _moveSpeed * Time.deltaTime);
        _rigitbody.MovePosition(movement);
    }

    private void Rotate()
    {
        Vector3 direction = (_target.position - _transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction * Time.deltaTime);
        rotation.x = 0;
        rotation.z = 0;
        _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _rotateSpeed);
    }

    private void Follow()
    {
        float distance = Vector3.Distance(_transform.position, _target.position);

        if (_approachDistance < distance)
        {
            Move();
            Moved?.Invoke(true);
        }
        else
        {
            Moved?.Invoke(false);
        }

        Rotate();
    }
}
