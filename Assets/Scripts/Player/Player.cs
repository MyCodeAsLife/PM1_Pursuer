using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerInputController _playerInputController;
    private AnimationController _animationController;
    private PlayerMover _mover;

    private void Awake()
    {
        var controller = GetComponent<CharacterController>();

        _playerInputController = new PlayerInputController();
        _animationController = new AnimationController(_animator);
        _mover = new PlayerMover(controller, transform);
    }

    private void OnEnable()
    {
        _mover.MoveStarted += OnStartRunning;
        _mover.MoveCanceled += OnCancelRunning;

        _playerInputController.SubscribeOnMoveStart(_mover.OnMoveStart);
        _playerInputController.SubscribeOnMovePerformed(_mover.OnMove);
        _playerInputController.SubscribeOnMoveCanceled(_mover.OnMoveCanceled);
    }

    private void OnDisable()
    {
        _mover.MoveStarted -= OnStartRunning;
        _mover.MoveCanceled -= OnCancelRunning;

        _playerInputController.UnSubscribeOnMoveStart(_mover.OnMoveStart);
        _playerInputController.UnSubscribeOnMovePerformed(_mover.OnMove);
        _playerInputController.UnSubscribeOnMoveCanceled(_mover.OnMoveCanceled);
    }

    private void Update()
    {
        _mover.Tick();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _mover.ColliderHit(hit);
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
