using Godot;

namespace Pigdom.Actors.States;

public partial class BumpingPigRunState : BumpingPigState
{
    public override void Enter()
    {
        Context.DirectionChanged += OnDirectionChanged;
        Context.PlayerDetected += OnPlayerDetected;
        Context.EnableAnimation("run");
    }

    public override void Exit()
    {
        Context.DirectionChanged -= OnDirectionChanged;
        Context.PlayerDetected -= OnPlayerDetected;
        Context.DisableAnimation("run");
    }

    public override void GetHurt()
    {
        Context.TransitionTo<BumpingPigHitState>();
    }

    private void OnDirectionChanged(int newDirection)
    {
        Context.FlipSprite(newDirection);

        if (newDirection == 0)
        {
            Context.TransitionTo<BumpingPigIdleState>();
        }
    }

    private void OnPlayerDetected(Area2D area)
    {
        Context.TransitionTo<BumpingPigAttackState>();
    }
}
