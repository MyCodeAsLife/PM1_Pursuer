using UnityEngine;

public class AnimationController
{
    private const string Running = "IsRunning";

    private Animator _animator;

    public AnimationController(Animator animator)
    {
        _animator = animator;
    }

    public bool IsRunning => _animator.GetBool(Running);

    public void SetRunning(bool isRunning)
    {
        _animator.SetBool(Running, isRunning);
    }
}
