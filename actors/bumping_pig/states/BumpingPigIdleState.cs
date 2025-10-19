using Godot;

namespace Pigdom.Actors.States;

public partial class BumpingPigIdleState : BumpingPigState
{
    public override void Enter()
    {
        Context.DirectionChanged += OnDirectionChanged;
        Context.PlayerDetected += OnPlayerDetected;
        Context.EnableAnimation("idle");
    }

    public override void Exit()
    {
        Context.DirectionChanged -= OnDirectionChanged;
        Context.PlayerDetected -= OnPlayerDetected;
        Context.DisableAnimation("idle");
    }

    public override void GetHurt()
    {
        Context.TransitionTo<BumpingPigHitState>();
    }

    private void OnDirectionChanged(int newDirection)
    {
        Context.FlipSprite(Context.CurrentDirection);

        if (newDirection != 0)
        {
            Context.TransitionTo<BumpingPigRunState>();
        }
    }

    private void OnPlayerDetected(Area2D area)
    {
        Context.TransitionTo<BumpingPigAttackState>();
    }
}
