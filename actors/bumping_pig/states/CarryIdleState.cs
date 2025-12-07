using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class CarryIdleState : State
{
    public override void Enter()
    {
        Context.Body.DirectionChanged += OnDirectionChanged;
        Context.AnimationTree.EnableCondition("idle");
    }

    public override void Exit()
    {
        Context.Body.DirectionChanged -= OnDirectionChanged;
        Context.AnimationTree.DisableCondition("idle");
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

        var bomb = Context.ThrowableFactory.Create();
        var body = Context.Body;
        var throwDirection = body.GlobalPosition.DirectionTo(bomb.GlobalPosition).X;

        if (bomb is RigidBody2D rigidBody)
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
            Context.TransitionTo<CarryRunState>();
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

    private void OnDirectionChanged(int newDirection)
    {
        Turn(Context.Body.Direction);

        if (newDirection != 0)
        {
            Context.TransitionTo<CarryRunState>();
        }
    }
}
