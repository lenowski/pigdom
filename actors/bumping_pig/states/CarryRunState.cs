using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class CarryRunState : State
{
    public override void Enter()
    {
        Context.Body.DirectionChanged += OnBodyDirectionChanged;
        Context.AnimationTree.EnableCondition("run");
    }

    public override void Exit()
    {
        Context.Body.DirectionChanged -= OnBodyDirectionChanged;
        Context.AnimationTree.DisableCondition("run");
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }

    public override async void Throw(Vector2 throwForce)
    {
        Context.TransitionTo<ThrowState>();

        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);

        var item = Context.ThrowableFactory.Create();
        var throwDirection = Context.Body.GlobalPosition.DirectionTo(item.GlobalPosition).X;
        if (item is RigidBody2D rigidBody)
        {
            throwForce = throwForce with { X = throwForce.X * throwDirection };
            rigidBody.ApplyCentralImpulse(throwForce);
        }
    }

    public override void Move(int direction)
    {
        if (direction != 0)
        {
            Turn(direction);
            Context.Body.Direction = direction;
        }
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

    public override void Stop()
    {
        Context.Body.Direction = 0;
        Context.TransitionTo<CarryIdleState>();
    }

    private void OnBodyDirectionChanged(int newDirection)
    {
        Turn(Context.Body.Direction);
        if (newDirection == 0)
        {
            Context.TransitionTo<CarryIdleState>();
        }
    }
}
