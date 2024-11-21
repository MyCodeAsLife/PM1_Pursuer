using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private EnemyMover _mover;
    private AnimationController _animationController;
    private Coroutine _following;

    private void Awake()
    {
        var rig = GetComponent<Rigidbody>();
        var animator = GetComponentInChildren<Animator>();

        _mover = new EnemyMover(_target, transform, rig);
        _animationController = new AnimationController(animator);
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
