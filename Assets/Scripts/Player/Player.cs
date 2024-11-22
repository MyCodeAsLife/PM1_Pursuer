using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private AnimationController _animationController;

    private void Awake()
    {
        var controller = GetComponent<CharacterController>();
        var animator = GetComponentInChildren<Animator>();

        _mover = new PlayerMover(controller, transform);
        _animationController = new AnimationController(animator);
    }

    private void OnEnable()
    {
        _mover.MoveStarted += OnStartRunning;
        _mover.MoveCanceled += OnCancelRunning;
    }

    private void OnDisable()
    {
        _mover.MoveStarted -= OnStartRunning;
        _mover.MoveCanceled -= OnCancelRunning;
    }

    private void Update()
    {
        _mover.Tick();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _mover.OnColliderHit(hit);
    }

    private void OnStartRunning()
    {
        if (_animationController.IsRunning == false)
            _animationController.SetRunning(true);
    }

    private void OnCancelRunning()
    {
        if (_animationController.IsRunning)
            _animationController.SetRunning(false);
    }
}
