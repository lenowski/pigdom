using Godot;
using Pigdom.Shared.Extensions;

namespace Pigdom.Actors.BumpingPig.States;

public partial class PickState : State
{
    public override async void Enter()
    {
        Context.AnimationTree.EnableCondition($"pick_{Context.PickingItemType}");
        await ToSignal(Context.AnimationTree, AnimationTree.SignalName.AnimationFinished);
        Context.TransitionTo<CarryRunState>();
    }

    public override void Exit()
    {
        Context.AnimationTree.DisableCondition($"pick_{Context.PickingItemType}");
        if (Context.PickingItemType == "bomb")
        {
            Context.AnimationTree.SetConditionPath("parameters/BombStateMachine/conditions");
        }
        else if (Context.PickingItemType == "crate")
        {
            Context.AnimationTree.SetConditionPath("parameters/CrateStateMachine/conditions");
        }
    }

    public override void GetHurt(int damage)
    {
        Context.Health -= damage;
        Context.TransitionTo<HitState>();
    }
}
