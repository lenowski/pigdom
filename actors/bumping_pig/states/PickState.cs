using Godot;
using Pigdom.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class PickState : State
{
    public override async void Enter()
    {
        Context.AnimationTree.EnableCondition("pick_bomb");
        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);
        Context.TransitionTo<CarryRunState>();
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition("pick_bomb");
        Context.AnimationTree.SetConditionPath("parameters/StateMachine/conditions");
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }
}
