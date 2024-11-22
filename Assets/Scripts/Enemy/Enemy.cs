using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;

    private EnemyMover _mover;
    private AnimationController _animationController;
    private Coroutine _following;

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();

        _mover = new EnemyMover(_target, transform, rigidbody);
        _animationController = new AnimationController(_animator);
    }

    private void OnEnable()
    {
        _mover.Moved += OnMove;
    }

    private void OnDisable()
    {
        _mover.Moved -= OnMove;
    }

    private void Update()
    {
        _mover.Tick();
    }

    private void OnMove(bool isMoving)
    {
        if (_animationController.IsRunning != isMoving)
            _animationController.SetRunning(isMoving);
    }
}
