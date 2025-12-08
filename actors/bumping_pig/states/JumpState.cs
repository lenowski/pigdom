using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class JumpState : State
{
    public override void _Ready()
    {
        base._Ready();
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Context.Body.Velocity.Y >= 0f)
        {
            Context.TransitionTo<FallState>();
        }
    }

    public override void Enter()
    {
        Context.AnimationTree.EnableCondition("jump");
        SetPhysicsProcess(true);
    }

    public override void Exit()
    {
        SetPhysicsProcess(false);
        Context.AnimationTree.DisableCondition("jump");
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }

    public override void CancelJump()
    {
        Context.Body.Velocity = Context.Body.Velocity with { Y = 0 };
    }

    public override void Move(int direction)
    {
        if (direction != 0)
        {
            Turn(direction);
            Context.Body.Direction = direction;
        }
    }

    public override void Stop()
    {
        Context.Body.Direction = 0;
    }

    public override void Turn(int direction)
    {
        var baseScale = Context.Sprites.Scale;

        Context.Sprites.Scale = direction switch
        {
            > 0 => baseScale with { X = -1 },
            < 0 => baseScale with { X = 1 },
            _ => baseScale,
        };
    }
}
