using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class FallState : State
{
    public override void _Ready()
    {
        base._Ready();
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Context.Body.IsOnFloor())
        {
            Context.TransitionTo(PreviousState);
        }
    }

    public override void Enter()
    {
        Context.AnimationTree.EnableCondition("fall");
        SetPhysicsProcess(true);
        PreviousState = PreviousState.PreviousState;
    }

    public override void Exit()
    {
        SetPhysicsProcess(false);
        Context.AnimationTree.DisableCondition("fall");
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
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
