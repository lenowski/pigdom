using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class AttackState : State
{
    public override async void Enter()
    {
        Context.AnimationTree.EnableCondition("attack");
        Context.Body.SetPhysicsProcess(false);
        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);
        Context.TransitionTo(PreviousState);
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition("attack");
        Context.Body.SetPhysicsProcess(true);
    }

    public override void GetHurt(int damage)
    {
        base.Context.Health -= damage;
        base.Context.TransitionTo<HitState>();
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
